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

            

            return View("Index", books);
        }


        [HttpGet]
        public IActionResult BookDetails()
        {
            var books = unitOfWork.Book.GetAll(null,"Category"); // Fetch data from your service/repository

            var result = books.Select(book => new
            {
                id = book.Id,
                productTitle = book.Title,
                price = book.Price,
                rating = book.Rating,
                category = book.Category.Name,
                author = book.Author
            });

            return Json(result);
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var categories = unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
            ).ToList();

            BookVM model = new BookVM
            {
                CategoryList = categories,
                Book = null// Initialize an empty book if id is null
            };

            if (id != null && id > 0)
            {
                var existingBook = unitOfWork.Book.Get(u => u.Id == id);
                if (existingBook != null)
                {
                    model.Book = existingBook;
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Upsert(BookVM model, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string rootPath = webHoshtEnv.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string path = Path.Combine(rootPath, @"images\Book");

                    if (!string.IsNullOrEmpty(model.Book.ImageUrl))
                    {
                        var existingImagePath = Path.Combine(rootPath, model.Book.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(existingImagePath))
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

                if (model.Book.Id != 0)
                {
                    unitOfWork.Book.Update(model.Book);
                }
                else
                {
                    unitOfWork.Book.Add(model.Book);
                }

                unitOfWork.Save();
                TempData["success"] = "Book saved successfully";
                return RedirectToAction("Index");
            }

            // Re-populate the dropdown if the form submission fails validation
            model.CategoryList = unitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                    Selected = u.Id == model.Book.CategoryId
                }
            ).ToList();

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
