using BulkyBook.DataAccess.Repository.Interface;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                var count = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == claim.Value).ToList().Count();
                HttpContext.Session.SetInt32(SessionKey.ShoppingCart, count);
            }
            return View(productList);
        }

        public IActionResult Details(int id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(item => item.Id == id, includeProperties: "Category,CoverType");
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                Product = product,
                ProductId = product.Id

            };
            return View(shoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart cartObj)
        {
            cartObj.Id = 0;
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                cartObj.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDatabase = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                    item => item.ApplicationUserId == cartObj.ApplicationUserId
                             && item.ProductId == cartObj.ProductId,
                    includeProperties: "Product");
                
                if(cartFromDatabase == null)
                {
                    _unitOfWork.ShoppingCart.Add(cartObj);
                }
                else
                {
                    cartFromDatabase.Count += cartObj.Count; 
                    _unitOfWork.ShoppingCart.Update(cartFromDatabase);
                }
                _unitOfWork.Save();

                var count = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == cartObj.ApplicationUserId).ToList().Count();
                HttpContext.Session.SetInt32(SessionKey.ShoppingCart, count);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                var product = _unitOfWork.Product.GetFirstOrDefault(item => item.Id == cartObj.ProductId, includeProperties: "Category,CoverType");
                ShoppingCart shoppingCart = new ShoppingCart()
                {
                    Product = product,
                    ProductId = product.Id

                };
                return View(shoppingCart);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
