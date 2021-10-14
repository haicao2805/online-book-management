
using FptBookStore.DataAccess.BaseRepository.Interface;
using FptBookStore.Entities;
using FptBookStore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FptBookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRole.Admin + "," + UserRole.Employee)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _unitOfWork.ApplicationUser.GetAll().ToList();
            var userRoles = _unitOfWork.IdentityUserRole.GetAll().ToList();
            var roles = _unitOfWork.IdentityRole.GetAll().ToList();

            foreach (var user in users)
            {
                var roleId = userRoles.FirstOrDefault(item => item.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(item => item.Id == roleId).Name;
            }

            return Json(new
            {
                data = users
            });
        }

        [HttpPost]
        public IActionResult LockOrUnlock([FromBody] string id)
        {
            var obj = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == id);
            var userRoles = _unitOfWork.IdentityUserRole.GetAll().ToList();
            var roles = _unitOfWork.IdentityRole.GetAll().ToList();

            if (obj == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            var roleId = userRoles.FirstOrDefault(item => item.UserId == obj.Id).RoleId;
            var role = roles.FirstOrDefault(item => item.Id == roleId).Name;
            
            if (role == UserRole.Admin)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            if (obj.LockoutEnd != null && obj.LockoutEnd > DateTime.Now)
            {
                // this current user is locked, we will unlock him/her
                obj.LockoutEnd = DateTime.Now;
            }
            else
            {
                obj.LockoutEnd = DateTime.Now.AddYears(2000);
            }
            _unitOfWork.ApplicationUser.Update(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Locking/Unlocking successful" });
        }
        #endregion
    }
}
