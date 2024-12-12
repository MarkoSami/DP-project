using Giftify.DAL.Repository.Interfaces;
using Giftify.Models;
using Giftify.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Diagnostics;

namespace Giftify.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index(List<int> selectedCategories, string sortOrder = "", string searchQuery = "")
        {
            // Get books as a queryable collection
            var books = unitOfWork.Book.GetAsQueryable();

            // Fetch related categories based on books
            var categoryIds = books.Select(b => b.CategoryId).Distinct().ToList();
            var categories = unitOfWork.Category.GetAll(c => categoryIds.Contains(c.Id));

            // Filter books by selected categories
            if (selectedCategories != null && selectedCategories.Any())
            {
                books = books.Where(b => selectedCategories.Contains(b.CategoryId));
            }

            // Filter books by search query
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                books = books.Where(b => b.Title.Contains(searchQuery) || b.Author.Contains(searchQuery));
            }

            // Apply sorting logic
            books = sortOrder switch
            {
                "price_asc" => books.OrderBy(b => b.Price),
                "price_desc" => books.OrderByDescending(b => b.Price),
                "rating_asc" => books.OrderBy(b => b.Rating),
                "rating_desc" => books.OrderByDescending(b => b.Rating),
                _ => books.OrderBy(b => b.Title) // Default sorting by title
            };

            // Prepare the ViewModel
            var model = new IndexViewModel
            {
                Categories = categories,
                Books = books.ToList(),
                SelectedCategoryIds = selectedCategories ?? new List<int>(),
                SortOrder = sortOrder,
                SearchQuery = searchQuery // Include the search query for the view
            };

            return View(model);
        }




        public IActionResult Details(int id)
        {
            var book = unitOfWork.Book.Get(u => u.Id == id);
            return View(book);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
