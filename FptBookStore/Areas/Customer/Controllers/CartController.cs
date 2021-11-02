using FptBookStore.DataAccess.BaseRepository.Interface;
using FptBookStore.Entities;
using FptBookStore.Utility;
using FptBookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Product = FptBookStore.Entities.Product;

namespace FptBookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = UserRole.User_Individual)]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public ShoppingCartViewModel ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork, IEmailSender emailSender, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == claim.Value);

            var listCart = HttpContext.Session.GetObject<List<(int, int)>>(SessionKey.ShoppingCartList);
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            if (listCart != null)
            {
                foreach (var cart in listCart)
                {
                    Product product = _unitOfWork.Product.GetFirstOrDefault(item => item.Id == cart.Item1, includeProperties: "Category");
                    ShoppingCart shoppingCart = new ShoppingCart()
                    {
                        Product = product,
                        ProductId = product.Id,
                        Count = cart.Item2,
                        ApplicationUser = user,
                        ApplicationUserId = user.Id,
                        Price = Helper.GetProductPriceBaseOnQuantity(cart.Item2, product.Price, product.Price50, product.Price100)
                    };
                    shoppingCarts.Add(shoppingCart);
                }
            }

            ShoppingCartVM = new ShoppingCartViewModel()
            {
                OrderHeader = new OrderHeader(),
                ListCart = shoppingCarts
            };
            ShoppingCartVM.OrderHeader.OrderTotal = 0;
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == claim.Value);

            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = Helper.GetProductPriceBaseOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Count * cart.Price);
                cart.Product.Description = Helper.ConvertToRawHtml(cart.Product.Description);
                if (cart.Product.Description.Length > 100)
                {
                    cart.Product.Description = cart.Product.Description.Substring(0, 99) + "...";
                }
            }

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> IndexPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == claim.Value);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verfication email is empty");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            ModelState.AddModelError(string.Empty, "Verfication email is sent. Please check your email");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Plus(int productId)
        {
            var listCart = HttpContext.Session.GetObject<List<(int, int)>>(SessionKey.ShoppingCartList);
            Product product = _unitOfWork.Product.GetFirstOrDefault(item => item.Id == productId, includeProperties: "Category");

            for (var i = 0; i < listCart.Count; i++)
            {
                if (listCart[i].Item1 == productId)
                {
                    if (product.Quantity < listCart[i].Item2 + 1)
                    {
                        ViewBag.Message = "Cannot add more than the remaining quantity";
                        return Redirect("/Customer/Cart/Index?errorMessage=Cannot add more " + product.Title);
                    }
                    else
                    {
                        listCart[i] = (listCart[i].Item1, listCart[i].Item2 + 1);
                    }
                }
            }

            HttpContext.Session.SetObject(SessionKey.ShoppingCartList, listCart);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int productId)
        {
            var listCart = HttpContext.Session.GetObject<List<(int, int)>>(SessionKey.ShoppingCartList);

            var numberOfCartOfUser = listCart.Count;
            int deleteCartIndex = -1;

            for (var i = 0; i < listCart.Count; i++)
            {
                if (listCart[i].Item1 == productId)
                {
                    if (listCart[i].Item2 == 1)
                    {
                        HttpContext.Session.SetInt32(SessionKey.ShoppingCartCount, numberOfCartOfUser - 1);
                        deleteCartIndex = i;
                        break;
                    }
                    else
                    {
                        listCart[i] = (listCart[i].Item1, listCart[i].Item2 - 1);
                    }
                }
            }

            if (deleteCartIndex != -1)
            {
                listCart.RemoveAt(deleteCartIndex);
            }

            HttpContext.Session.SetObject(SessionKey.ShoppingCartList, listCart);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int productId)
        {
            var listCart = HttpContext.Session.GetObject<List<(int, int)>>(SessionKey.ShoppingCartList);

            var numberOfCartOfUser = listCart.Count;
            int deleteCartIndex = -1;

            for (var i = 0; i < listCart.Count; i++)
            {
                if (listCart[i].Item1 == productId)
                {
                    deleteCartIndex = i;
                    break;
                }
            }
            listCart.RemoveAt(deleteCartIndex);

            HttpContext.Session.SetInt32(SessionKey.ShoppingCartCount, numberOfCartOfUser - 1);
            HttpContext.Session.SetObject(SessionKey.ShoppingCartList, listCart);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == claim.Value);

            var listCart = HttpContext.Session.GetObject<List<(int, int)>>(SessionKey.ShoppingCartList);
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            foreach (var cart in listCart)
            {
                Product product = _unitOfWork.Product.GetFirstOrDefault(item => item.Id == cart.Item1, includeProperties: "Category");
                ShoppingCart shoppingCart = new ShoppingCart()
                {
                    Product = product,
                    ProductId = product.Id,
                    Count = cart.Item2,
                    ApplicationUser = user,
                    ApplicationUserId = user.Id,
                    Price = Helper.GetProductPriceBaseOnQuantity(cart.Item2, product.Price, product.Price50, product.Price100)
                };
                shoppingCarts.Add(shoppingCart);
            }

            ShoppingCartVM = new ShoppingCartViewModel()
            {
                OrderHeader = new OrderHeader(),
                ListCart = shoppingCarts
            };

            ShoppingCartVM.OrderHeader.ApplicationUser = user;
            ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = Helper.GetProductPriceBaseOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Count * cart.Price);
                cart.Product.Description = Helper.ConvertToRawHtml(cart.Product.Description);
                if (cart.Product.Description.Length > 100)
                {
                    cart.Product.Description = cart.Product.Description.Substring(0, 99) + "...";
                }
            }

            ShoppingCartVM.OrderHeader.Name = ShoppingCartVM.OrderHeader.ApplicationUser.Name;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

            return View(ShoppingCartVM);
        }

        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPOST(string stripeToken)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == claim.Value);

            var listCart = HttpContext.Session.GetObject<List<(int, int)>>(SessionKey.ShoppingCartList);
            List<ShoppingCart> shoppingCarts = new List<ShoppingCart>();
            foreach (var cart in listCart)
            {
                Product product = _unitOfWork.Product.GetFirstOrDefault(item => item.Id == cart.Item1, includeProperties: "Category");
                ShoppingCart shoppingCart = new ShoppingCart()
                {
                    Product = product,
                    ProductId = product.Id,
                    Count = cart.Item2,
                    ApplicationUser = user,
                    ApplicationUserId = user.Id,
                    Price = Helper.GetProductPriceBaseOnQuantity(cart.Item2, product.Price, product.Price50, product.Price100)
                };
                shoppingCarts.Add(shoppingCart);
            }

            ShoppingCartVM.ListCart = shoppingCarts;
            ShoppingCartVM.OrderHeader.PaymentStatus = PaymentStatus.Pending;
            ShoppingCartVM.OrderHeader.OrderStatus = Status.Pending;
            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;

            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            foreach (var item in ShoppingCartVM.ListCart)
            {
                item.Price = Helper.GetProductPriceBaseOnQuantity(item.Count, item.Product.Price, item.Product.Price50, item.Product.Price100);
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = item.ProductId,
                    OrderId = ShoppingCartVM.OrderHeader.Id,
                    Price = item.Price,
                    Count = item.Count
                };

                Product product = _unitOfWork.Product.GetFirstOrDefault(p => p.Id == item.ProductId);
                product.Quantity = product.Quantity - item.Count;
                _unitOfWork.Product.Update(product);
                ShoppingCartVM.OrderHeader.OrderTotal += orderDetail.Price * orderDetail.Count;
                _unitOfWork.OrderDetail.Add(orderDetail);
            }

            HttpContext.Session.SetObject(SessionKey.ShoppingCartList, null);
            HttpContext.Session.SetInt32(SessionKey.ShoppingCartCount, 0);
            _unitOfWork.Save();

            if (stripeToken == null)
            {
                // order will be created for delayed payment for authorized company
                ShoppingCartVM.OrderHeader.PaymentDueDate = DateTime.Now.AddDays(30);
                ShoppingCartVM.OrderHeader.PaymentStatus = PaymentStatus.DelayedPayment;
                ShoppingCartVM.OrderHeader.OrderStatus = Status.Approved;
            }
            else
            {
                var options = new ChargeCreateOptions()
                {
                    Amount = Convert.ToInt32(ShoppingCartVM.OrderHeader.OrderTotal * 100),
                    Currency = "usd",
                    Description = "Order ID: " + ShoppingCartVM.OrderHeader.Id,
                    Source = stripeToken
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);

                if (charge.BalanceTransactionId == null)
                {
                    ShoppingCartVM.OrderHeader.PaymentStatus = PaymentStatus.Rejected;
                }
                else
                {
                    ShoppingCartVM.OrderHeader.TransactionId = charge.Id;
                }

                if (charge.Status.ToLower() == "succeeded")
                {
                    ShoppingCartVM.OrderHeader.PaymentStatus = PaymentStatus.Approved;
                    ShoppingCartVM.OrderHeader.OrderStatus = Status.Approved;
                    ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
                }
            }

            _unitOfWork.Save();

            return RedirectToAction("OrderConfirmation", "Cart", new { id = ShoppingCartVM.OrderHeader.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
    }
}
