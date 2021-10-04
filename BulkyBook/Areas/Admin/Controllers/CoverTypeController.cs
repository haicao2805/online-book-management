using BulkyBook.DataAccess.Repository.Interface;
using BulkyBook.Models;
using BulkyBook.Utility;
using Dapper;
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
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null)
            {
                return View(coverType);
            }

            // cách viết bình thường 
            // coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            // cách viết dùng stored procedure 
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id.GetValueOrDefault());
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(ProcedureName.Proc_CoverType_Get, parameter);

            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                if (coverType.Id == 0)
                {
                    // cách viết bình thường 
                    // _unitOfWork.CoverType.Add(coverType);
                    // cách viết dùng stored procedure 
                    var parameter = new DynamicParameters();
                    parameter.Add("@Name", coverType.Name);
                    _unitOfWork.SP_Call.Execute(ProcedureName.Proc_CoverType_Create, parameter);
                }
                else
                {
                    // cách viết bình thường 
                    // _unitOfWork.CoverType.Update(coverType);
                    // cách viết dùng stored procedure 
                    var parameter = new DynamicParameters();
                    parameter.Add("@Id", coverType.Id);
                    parameter.Add("@Name", coverType.Name);
                    _unitOfWork.SP_Call.Execute(ProcedureName.Proc_CoverType_Update, parameter);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            // cách viết bình thường 
            // var allCoverTypes = _unitOfWork.CoverType.GetAll();
            // cách viết dùng stored procedure 
            var allCoverTypes = _unitOfWork.SP_Call.List<CoverType>(ProcedureName.Proc_CoverType_GetAll, null);
            return Json(new
            {
                data = allCoverTypes
            });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            // cách viết bình thường 
            // var var obj = _unitOfWork.CoverType.Get(id);
            // cách viết dùng stored procedure 
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            var obj = _unitOfWork.SP_Call.OneRecord<CoverType>(ProcedureName.Proc_CoverType_Get, parameter);
            if (obj == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }
            // cách viết bình thường, xóa bằng entity 
            // _unitOfWork.CoverType.Remove(obj);
            // cách viết dùng stored procedure, xóa bằng id 
            _unitOfWork.SP_Call.Execute(ProcedureName.Proc_CoverType_Delete, parameter);
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
