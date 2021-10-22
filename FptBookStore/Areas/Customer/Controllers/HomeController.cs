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

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");

            var listCart = HttpContext.Session.GetObject<List<(int, int)>>(SessionKey.ShoppingCartList);
            HttpContext.Session.SetInt32(SessionKey.ShoppingCartCount, listCart == null ? 0 : listCart.Count);

            return View(productList);
        }

        [Authorize(Roles = UserRole.User_Individual)]
        public IActionResult Details(int id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(item => item.Id == id, includeProperties: "Category");
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                Product = product,
                ProductId = product.Id

            };
            return View(shoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRole.User_Individual)]
        public IActionResult Details(ShoppingCart cartObj)
        {
            if (ModelState.IsValid)
            {
                Product product = _unitOfWork.Product.GetFirstOrDefault(item => item.Id == cartObj.ProductId, includeProperties: "Category");

                List<(int, int)> listCart = HttpContext.Session.GetObject<List<(int, int)>>(SessionKey.ShoppingCartList);
                if (listCart == null)
                {
                    if (product.Quantity < cartObj.Count)
                    {
                        ViewData["errorMessage"] = "Cannot add more than the remaining quantity";
                        ShoppingCart shoppingCart = new ShoppingCart()
                        {
                            Product = product,
                            ProductId = product.Id

                        };
                        return View(shoppingCart);
                    }
                    else
                    {
                        listCart = new List<(int, int)>();
                        listCart.Add((product.Id, cartObj.Count));
                    }
                }
                else
                {
                    bool IsContainThisBook = false;
                    for (int i = 0; i < listCart.Count; i++)
                    {
                        if (listCart[i].Item1 == product.Id)
                        {
                            if (product.Quantity < listCart[i].Item2 + cartObj.Count)
                            {
                                ViewData["errorMessage"] = "Cannot add more than the remaining quantity";
                                ShoppingCart shoppingCart = new ShoppingCart()
                                {
                                    Product = product,
                                    ProductId = product.Id

                                };
                                return View(shoppingCart);
                            }
                            else
                            {
                                listCart[i] = (listCart[i].Item1, listCart[i].Item2 + cartObj.Count);
                            }
                            IsContainThisBook = true;
                            break;
                        }
                    }
                    if (!IsContainThisBook)
                    {
                        if (product.Quantity < cartObj.Count)
                        {
                            ViewData["errorMessage"] = "Cannot add more than the remaining quantity";
                            ShoppingCart shoppingCart = new ShoppingCart()
                            {
                                Product = product,
                                ProductId = product.Id

                            };
                            return View(shoppingCart);
                        }
                        else
                        {
                            listCart.Add((product.Id, cartObj.Count));
                        }
                    }
                }

                HttpContext.Session.SetObject(SessionKey.ShoppingCartList, listCart);
                HttpContext.Session.SetInt32(SessionKey.ShoppingCartCount, listCart.Count);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                var product = _unitOfWork.Product.GetFirstOrDefault(item => item.Id == cartObj.ProductId, includeProperties: "Category");
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
