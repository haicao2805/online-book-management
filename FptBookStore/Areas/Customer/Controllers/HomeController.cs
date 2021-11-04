using FptBookStore.Areas.Customer.ViewModels;
using FptBookStore.DataAccess.BaseRepository.Interface;
using FptBookStore.Entities;
using FptBookStore.Utility;
using FptBookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PagedList;
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
        public IActionResult Index(int? categoryId, int? page)
        {
            Console.WriteLine("CategoryId: " + categoryId);
            IEnumerable<Product> productList;
            List<Product> newestProductList = new List<Product>();

            if (categoryId != null)
            {
                productList = _unitOfWork.Product.GetAll((product => product.CategoryId == (categoryId)), includeProperties: "Category");
            }
            else
            {
                productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
            }
            //Pagination
            int pageSize = 2;
            productList.ToPagedList(page ?? 1, pageSize);

            newestProductList = _unitOfWork.Product.GetAll().OrderBy(product => product.CreatedDate).ToList();

            if (newestProductList.Count() > 15)
            {
                newestProductList.RemoveRange(14, newestProductList.Count() - 15);
            }


            IEnumerable<Category> categories = _unitOfWork.Category.GetAll();
            var listCart = HttpContext.Session.GetObject<List<(int, int)>>(SessionKey.ShoppingCartList);
            HttpContext.Session.SetInt32(SessionKey.ShoppingCartCount, listCart == null ? 0 : listCart.Count);
            var viewModel = new HomeViewModel(productList.ToList().ToPagedList(page ?? 1, pageSize), categories.ToList(), newestProductList);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRole.Customer)]
        public IActionResult Index(HomeViewModel model)
        {
            AddToCartInput cartObj = model.AddToCartInput;
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
    }
}
