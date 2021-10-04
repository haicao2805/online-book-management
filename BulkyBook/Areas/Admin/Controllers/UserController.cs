using BulkyBook.DataAccess.Repository.Interface;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
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
            var users = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();
            var userRoles = _unitOfWork.IdentityUserRole.GetAll().ToList();
            var roles = _unitOfWork.IdentityRole.GetAll().ToList();

            foreach(var user in users)
            {
                var roleId = userRoles.FirstOrDefault(item => item.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(item => item.Id == roleId).Name;
                if(user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }

            return Json(new
            {
                data = users
            });
        }

        [HttpPost]
        public IActionResult LockOrUnlock([FromBody]string id)
        {
            var obj = _unitOfWork.ApplicationUser.GetFirstOrDefault(item => item.Id == id);
            if(obj == null)
            {
                return Json(new { success = false, message="Error while Locking/Unlocking" });
            }

            if(obj.LockoutEnd != null && obj.LockoutEnd > DateTime.Now)
            {
                // this current user is locked, we will unlock him/her
                obj.LockoutEnd = DateTime.Now;
            }
            else
            {
                obj.LockoutEnd = DateTime.Now.AddYears(2000);
            }
            _unitOfWork.Save();
            return Json(new { success = true, message = "Locking/Unlocking successful" });
        }
        #endregion
    }
}
