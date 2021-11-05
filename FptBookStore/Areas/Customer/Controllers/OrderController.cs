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

namespace FptBookStore.Areas.Customer.Controllers
{
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

            IEnumerable<OrderHeader> ordList = null;
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
            return View(ordList);
        }
    }
}
