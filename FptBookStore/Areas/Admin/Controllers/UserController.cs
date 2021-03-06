
using FptBookStore.DataAccess.BaseRepository.Interface;
using FptBookStore.Entities;
using FptBookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FptBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRole.Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationUser> usersList = _unitOfWork.ApplicationUser.GetAll();
            return View(usersList);
        }



        [HttpPost]
        public IActionResult LockOrUnlock(string id)
        {
            IEnumerable<ApplicationUser> usersList = _unitOfWork.ApplicationUser.GetAll();
            var obj = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == id);
            var userRoles = _unitOfWork.IdentityUserRole.GetAll().ToList();
            var roles = _unitOfWork.IdentityRole.GetAll().ToList();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var loginUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == claim.Value);
            var roleLoginUserId = userRoles.FirstOrDefault(item => item.UserId == loginUser.Id).RoleId;
            var roleLoginUser = roles.FirstOrDefault(item => item.Id == roleLoginUserId).Name;

            if (obj == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            var roleId = userRoles.FirstOrDefault(item => item.UserId == obj.Id).RoleId;
            var role = roles.FirstOrDefault(item => item.Id == roleId).Name;

            if (role == UserRole.Admin)
            {
                return View("Index", usersList);
            }

            if (obj.LockoutEnd != null && obj.LockoutEnd > DateTime.Now)
            {
                obj.LockoutEnd = DateTime.Now;
            }
            else
            {
                obj.LockoutEnd = DateTime.Now.AddYears(2000);
            }
            _unitOfWork.ApplicationUser.Update(obj);
            _unitOfWork.Save();
            return View("Index", usersList);
        }
    }
}
