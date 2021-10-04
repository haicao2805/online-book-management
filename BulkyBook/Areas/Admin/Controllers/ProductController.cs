using BulkyBook.DataAccess.Repository.Interface;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = UserRole.Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnv;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _webHostEnv = env;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProductViewModel productViewModel = new ProductViewModel
            {
                Product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(category => new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(coverType => new SelectListItem
                {
                    Text = coverType.Name,
                    Value = coverType.Id.ToString()
                })
            };

            if (id == null)
            {
                return View(productViewModel);
            }

            productViewModel.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (productViewModel.Product == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _webHostEnv.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploadFolder = Path.Combine(webRootPath, "image/product");
                    var extension = Path.GetExtension(files[0].FileName);

                    if (productViewModel.Product.ImageUrl != null)
                    {
                        // Update image mới, ta xóa image cái cũ và update image mới
                        var imagePath = Path.Combine(webRootPath, productViewModel.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploadFolder, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    productViewModel.Product.ImageUrl = @"\image\product\" + fileName + extension;
                }
                else
                {
                    // Khi update product nhưng không thay đổi image 
                    if (productViewModel.Product.Id != 0)
                    {
                        var obj = _unitOfWork.Product.Get(productViewModel.Product.Id);
                        productViewModel.Product.ImageUrl = obj.ImageUrl;
                    }
                }

                if (productViewModel.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productViewModel.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productViewModel.Product);
                }

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productViewModel.CategoryList = _unitOfWork.Category.GetAll().Select(category => new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
                productViewModel.CoverTypeList = _unitOfWork.CoverType.GetAll().Select(coverType => new SelectListItem
                {
                    Text = coverType.Name,
                    Value = coverType.Id.ToString()
                });
                if (productViewModel.Product.Id != 0)
                {
                    productViewModel.Product = _unitOfWork.Product.Get(productViewModel.Product.Id);
                }
            }

            return View(productViewModel);
        }

        #region API Call
        [HttpGet]
        public IActionResult GetAll()
        {
            var allProducts = _unitOfWork.Product.GetAll(includeProperties: "Category,CoverType");

            return Json(new
            {
                data = allProducts
            });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Product.Get(id);
            if (obj == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }

            if (obj.ImageUrl != null)
            {
                string webRootPath = _webHostEnv.WebRootPath;
                var imagePath = Path.Combine(webRootPath, obj.ImageUrl.TrimStart('\\'));
                Console.WriteLine(imagePath);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _unitOfWork.Product.Remove(obj);
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
