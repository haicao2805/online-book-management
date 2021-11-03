using FptBookStore.Areas.Customer.ViewModels;
using FptBookStore.DataAccess.BaseRepository.Interface;
using FptBookStore.Entities;
using FptBookStore.Utility;
using FptBookStore.ViewModels;
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

namespace FptBookStore.Areas.Customer.Controllers
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

        [HttpGet]
        public IActionResult Index(int? categoryId)
        {
            Console.WriteLine("CategoryId: " + categoryId);
            IEnumerable<Product> productList;
            if (categoryId != null)
            {
                productList = _unitOfWork.Product.GetAll((product => product.CategoryId == (categoryId)), includeProperties: "Category");
            }
            else
            {
                productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            }

            IEnumerable<Category> categories = _unitOfWork.Category.GetAll();
            var listCart = HttpContext.Session.GetObject<List<(int, int)>>(SessionKey.ShoppingCartList);
            HttpContext.Session.SetInt32(SessionKey.ShoppingCartCount, listCart == null ? 0 : listCart.Count);
            var viewModel = new HomeViewModel(productList.ToList(), categories.ToList());
            return View(viewModel);
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
