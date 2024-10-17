using Giftify.DAL.Repository;
using Giftify.DAL.Repository.Interfaces;
using Giftify.Models;
using Giftify.Models.ViewModels;
using Giftify.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Giftify.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.ADMIN)]

    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHoshtEnv;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHoshtEnv)
        {
            this.unitOfWork = unitOfWork;
            this.webHoshtEnv = webHoshtEnv;
        }
        public IActionResult Index()
        {
            List<Product> products = unitOfWork.Product.GetAll("Category");
            return View("Index", products);
        }
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> categories = unitOfWork.Category.GetAll().Select(
                u=> new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
                );
            ProductVM model = new ProductVM
            {
                CategoryList = categories,
                Product = new Product()
            };
            if(id== null || id == 0 )
            {
                return View(model);
            }
            else
            {
                model.Product = unitOfWork.Product.Get((u) => u.Id == id);
                return View(model);
            }

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string rootPath = webHoshtEnv.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(rootPath, @"images\Product");

                    if(!string.IsNullOrEmpty(model.Product.ImageURL))
                    {
                        var existingImagePath = Path.Combine(rootPath, model.Product.ImageURL.TrimStart('\\'));
                        if(System.IO.File.Exists(existingImagePath))
                        {
                            System.IO.File.Delete(existingImagePath); 
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    model.Product.ImageURL = @"\images\Product\" + fileName;
                }
                if(model.Product.Id != 0) {

                unitOfWork.Product.Update(model.Product);
                }
                else
                {
                    unitOfWork.Product.Add(model.Product);
                }
                unitOfWork.Save();
                TempData["success"] = "Product Created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                model.CategoryList = unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
                );
            }
            return View(model);
        }
       
        public IActionResult Delete(int id)
        {
            Product productToDelete = unitOfWork.Product.Get(u => u.Id == id);
            if (productToDelete == null) return NotFound();

            unitOfWork.Product.Remove(productToDelete);
            unitOfWork.Save();
            return RedirectToAction("Index");

        }



        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> products = unitOfWork.Product.GetAll("Category");
            return Json(new {data = products });
        }
        #endregion
    }
}
