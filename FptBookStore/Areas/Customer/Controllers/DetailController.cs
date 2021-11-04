using System;
using System.Collections.Generic;
using FptBookStore.Areas.Customer.ViewModels;
using FptBookStore.DataAccess.BaseRepository.Interface;
using FptBookStore.Entities;
using FptBookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FptBookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class DetailController : Controller
    {
        private readonly ILogger<DetailController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public DetailController(ILogger<DetailController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = UserRole.Customer)]
        public IActionResult Index(int id)
        {
            var product = _unitOfWork.Product.GetFirstOrDefault(item => item.Id == id, includeProperties: "Category");
            DetailViewModel viewModel = new DetailViewModel()
            {
                Product = product,
                AddToCartInput = new AddToCartInput(id, 0)
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = UserRole.Customer + ", " + UserRole.Admin)]
        public IActionResult Index(DetailViewModel model)
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
