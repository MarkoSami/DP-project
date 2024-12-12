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

    public class BookController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHoshtEnv;

        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webHoshtEnv)
        {
            this.unitOfWork = unitOfWork;
            this.webHoshtEnv = webHoshtEnv;
        }
        public IActionResult Index()
        {
            var books = unitOfWork.Book.GetAll(null, "Category");
            var categories = unitOfWork.Category
                .GetAll(c => books.Any(b => b.CategoryId == c.Id));
            var indexVM = new IndexViewModel
            {
                Books = books,
                Categories = categories
            };

            return View("Index", indexVM);
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
            BookVM model = new BookVM
            {
                CategoryList = categories,
                Book = null
            };
            if(id== null || id == 0 )
            {
                return View(model);
            }
            else
            {
                model.Book = unitOfWork.Book.Get((u) => u.Id == id);
                return View(model);
            }

        }
        [HttpPost]
        public IActionResult Upsert(BookVM model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string rootPath = webHoshtEnv.WebRootPath;
                if(file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(rootPath, @"images\Book");

                    if(!string.IsNullOrEmpty(model.Book.ImageUrl))
                    {
                        var existingImagePath = Path.Combine(rootPath, model.Book.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(existingImagePath))
                        {
                            System.IO.File.Delete(existingImagePath); 
                        }
                    }

                    using (var filestream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    model.Book.ImageUrl = @"\images\Book\" + fileName;
                }
                if(model.Book.Id != 0) {

                unitOfWork.Book.Update(model.Book);
                }
                else
                {
                    unitOfWork.Book.Add(model.Book);
                }
                unitOfWork.Save();
                TempData["success"] = "Book Created successfully";
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
            Book BookToDelete = unitOfWork.Book.Get(u => u.Id == id);
            if (BookToDelete == null) return NotFound();

            unitOfWork.Book.Remove(BookToDelete);
            unitOfWork.Save();
            return RedirectToAction("Index");

        }



        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Book> Books = unitOfWork.Book.GetAll(null, "Category");
            return Json(new {data = Books });
        }
        #endregion
    }
}
