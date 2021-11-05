using FptBookStore.DataAccess.BaseRepository.Interface;
using FptBookStore.Entities;
using FptBookStore.Utility;
using FptBookStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FptBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public OrderDetailViewModel OrderDetailVM { get; set; }
        public IActionResult Index(string? status)
        {

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser user = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == claim.Value);
            var isAdmin = User.IsInRole(UserRole.Admin);

            IEnumerable<OrderHeader> ordList = null;
            //Admin
            if (isAdmin)
            {
                if (status != null || status == "")
                {
                    if (status.ToUpper() != "ALL")
                    {
                        ordList = _unitOfWork.OrderHeader.GetAll().Where(item => item.OrderStatus.ToUpper().Equals(status.ToUpper()));
                    }
                    else
                    {
                        ordList = _unitOfWork.OrderHeader.GetAll();
                    }
                }
                else
                {
                    ordList = _unitOfWork.OrderHeader.GetAll();
                }
            }
            else if (!isAdmin && (user != null))
            {
                if (status != null || status == "")
                {
                    if (status.ToUpper() != "ALL")
                    {
                        ordList = _unitOfWork.OrderHeader.GetAll().Where(item => item.OrderStatus.ToUpper().Equals(status.ToUpper()) && item.ApplicationUserId.Equals(user.Id));
                    }
                    else
                    {
                        ordList = _unitOfWork.OrderHeader.GetAll().Where(item => item.ApplicationUserId.Equals(user.Id));
                    }
                }
                else
                {
                    ordList = _unitOfWork.OrderHeader.GetAll().Where(item => item.ApplicationUserId.Equals(user.Id));
                }
            }

            return View(ordList);
        }

        public IActionResult Detail(int id)
        {
            OrderDetailVM = new OrderDetailViewModel()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(item => item.Id == id, includeProperties: "ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetail.GetAll(item => item.OrderId == id, includeProperties: "Product")
            };
            return View(OrderDetailVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Detail")]
        public IActionResult DetailPOST(string stripeToken)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(item => item.Id == OrderDetailVM.OrderHeader.Id, includeProperties: "ApplicationUser");

            if (stripeToken != null)
            {
                var options = new ChargeCreateOptions()
                {
                    Amount = Convert.ToInt32(orderHeader.OrderTotal * 100),
                    Currency = "usd",
                    Description = "Order ID: " + orderHeader.Id,
                    Source = stripeToken
                };

                var service = new ChargeService();
                Charge charge = service.Create(options);

                if (charge.BalanceTransactionId == null)
                {
                    orderHeader.PaymentStatus = PaymentStatus.Rejected;
                }
                else
                {
                    orderHeader.TransactionId = charge.Id;
                }

                if (charge.Status.ToLower() == "succeeded")
                {
                    orderHeader.PaymentStatus = PaymentStatus.Approved;
                    orderHeader.OrderDate = DateTime.Now;
                }

                _unitOfWork.OrderHeader.Update(orderHeader);
                _unitOfWork.Save();
            }

            return RedirectToAction("Detail", "Order", new { id = orderHeader.Id });
        }

        [Authorize(Roles = UserRole.Admin)]
        public IActionResult StartProcessing(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(item => item.Id == id);
            orderHeader.OrderStatus = Status.Processing;
            _unitOfWork.OrderHeader.Update(orderHeader);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Admin)]
        public IActionResult ShipOrder()
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(item => item.Id == OrderDetailVM.OrderHeader.Id);
            orderHeader.TrackingNumber = OrderDetailVM.OrderHeader.TrackingNumber;
            orderHeader.Carrier = OrderDetailVM.OrderHeader.Carrier;
            orderHeader.ShippingDate = DateTime.Now;
            orderHeader.OrderStatus = Status.Shipped;

            _unitOfWork.OrderHeader.Update(orderHeader);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = UserRole.Admin)]
        public IActionResult CancelOrder(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(item => item.Id == id);
            if (orderHeader.PaymentStatus == PaymentStatus.Approved)
            {
                var options = new RefundCreateOptions()
                {
                    Amount = Convert.ToInt32(orderHeader.OrderTotal * 100),
                    Reason = RefundReasons.RequestedByCustomer,
                    Charge = orderHeader.TransactionId
                };
                var service = new RefundService();
                Refund refund = service.Create(options);

                orderHeader.PaymentStatus = PaymentStatus.Refunded;
                orderHeader.OrderStatus = Status.Refunded;
            }
            else
            {
                orderHeader.OrderStatus = Status.Canceled;
                orderHeader.PaymentStatus = PaymentStatus.Canceled;
            }

            _unitOfWork.OrderHeader.Update(orderHeader);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetOrderList(string status)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<OrderHeader> orders;
            if (User.IsInRole(UserRole.Admin))
            {
                orders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {
                orders = _unitOfWork.OrderHeader.GetAll(item => item.ApplicationUserId == claim.Value, includeProperties: "ApplicationUser");
            }

            switch (status)
            {
                case "inprocess":
                    orders = orders.Where(item => (item.OrderStatus == Status.Processing ||
                                         item.PaymentStatus == PaymentStatus.Approved) &&
                                         item.OrderStatus != Status.Shipped);
                    break;
                case "compeleted":
                    orders = orders.Where(item => item.OrderStatus == Status.Shipped);
                    break;
                case "rejected":
                    orders = orders.Where(item => item.OrderStatus == Status.Refunded ||
                                         item.OrderStatus == Status.Canceled ||
                                         item.OrderStatus == Status.Rejected ||
                                         item.PaymentStatus == PaymentStatus.Refunded ||
                                         item.PaymentStatus == PaymentStatus.Canceled ||
                                         item.PaymentStatus == PaymentStatus.Rejected);
                    break;
                default:
                    break;
            }

            return Json(new
            {
                data = orders
            });
        }
        #endregion
    }
}
