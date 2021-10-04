using BulkyBook.DataAccess.Repository.Interface;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Customer.Controllers
{
    [Area("Customer")]
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

            ShoppingCartVM = new ShoppingCartViewModel()
            {
                OrderHeader = new OrderHeader(),
                ListCart = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == claim.Value, includeProperties: "Product")
            };
            ShoppingCartVM.OrderHeader.OrderTotal = 0;
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == claim.Value,
                                                                                                        includeProperties: "Company");

            foreach(var cart in ShoppingCartVM.ListCart)
            {
                cart.Price = Helper.GetProductPriceBaseOnQuantity(cart.Count, cart.Product.Price, cart.Product.Price50, cart.Product.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Count * cart.Price);
                cart.Product.Description = Helper.ConvertToRawHtml(cart.Product.Description);
                if(cart.Product.Description.Length > 100)
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
            if(user == null)
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

        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(item => item.Id == cartId, includeProperties: "Product");
            cart.Count += 1;
            cart.Price = Helper.GetProductPriceBaseOnQuantity(cart.Count, cart.Product.Price, 
                                                           cart.Product.Price50, cart.Product.Price100);
            _unitOfWork.ShoppingCart.Update(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(item => item.Id == cartId, includeProperties: "Product");
            
            if(cart.Count == 1)
            {
                var numberOfCartOfUser = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == cart.ApplicationUserId).ToList().Count();
                _unitOfWork.ShoppingCart.Remove(cart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SessionKey.ShoppingCart, numberOfCartOfUser - 1);
            }
            else
            {
                cart.Count -= 1;
                cart.Price = Helper.GetProductPriceBaseOnQuantity(cart.Count, cart.Product.Price,
                                                                           cart.Product.Price50, cart.Product.Price100);
                _unitOfWork.ShoppingCart.Update(cart);
                _unitOfWork.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int cartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(item => item.Id == cartId, includeProperties: "Product");

            var numberOfCartOfUser = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == cart.ApplicationUserId).ToList().Count();
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(SessionKey.ShoppingCart, numberOfCartOfUser - 1);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            
            ShoppingCartVM = new ShoppingCartViewModel()
            {
                OrderHeader = new OrderHeader(),
                ListCart = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == claim.Value,
                                                           includeProperties: "Product")
            };
            
            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == claim.Value, 
                                                                                                        includeProperties: "Company");
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
        public IActionResult SummaryPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(
                                                            item => item.Id == claim.Value, includeProperties: "Company");

            ShoppingCartVM.ListCart = _unitOfWork.ShoppingCart.GetAll(item => item.ApplicationUserId == claim.Value, includeProperties: "Product");
            ShoppingCartVM.OrderHeader.PaymentStatus = PaymentStatus.Pending;
            ShoppingCartVM.OrderHeader.OrderStatus = Status.Pending;
            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;

            _unitOfWork.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach(var item in ShoppingCartVM.ListCart)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    ProductId = item.ProductId,
                    OrderId = ShoppingCartVM.OrderHeader.Id,
                    Price = item.Price,
                    Count = item.Count
                };

                ShoppingCartVM.OrderHeader.OrderTotal += orderDetail.Price * orderDetail.Count;
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }

            _unitOfWork.ShoppingCart.Remove(ShoppingCartVM.ListCart);
            HttpContext.Session.SetInt32(SessionKey.ShoppingCart, 0);
            _unitOfWork.Save();

            return RedirectToAction("OrderConfirmation", "Cart", new { id = ShoppingCartVM.OrderHeader.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
    }
}
