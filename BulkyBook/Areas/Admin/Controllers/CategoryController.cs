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
    [Authorize(Roles = UserRole.Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null)
            {
                return View(category);
            }

            category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == 0)
                {
                    _unitOfWork.Category.Add(category);
                }
                else
                {
                    _unitOfWork.Category.Update(category);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var allCategories = _unitOfWork.Category.GetAll(orderBy: query => query.OrderBy(c => c.Name));
            return Json(new
            {
                data = allCategories
            });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Category.Get(id);
            if (obj == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            return Json(new
            {
                success = true,
                message = "Delete successful"
            });
        }
        #endregion
    }
}
