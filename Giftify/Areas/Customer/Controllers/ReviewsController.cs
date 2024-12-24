using Giftify.DataAccess.Data;
using Giftify.Models;
using Giftify.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Giftify.Areas.Customer.Controllers
{
    [Area("Customer")]

    public class ReviewsController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index(int bookId)
        {
            var reviews = await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .Where(r => r.BookId == bookId)
                .ToListAsync();
            ViewBag.BookId = bookId;
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            ViewBag.UserId = userId;

            return PartialView("_ReviewsList", reviews);
        }

        // GET: Reviews/Create
        public IActionResult Create(int bookId)
        {
            ViewBag.BookId = bookId;
            return PartialView("_CreateReview");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,BookId,Content,Rating")] ReviewVM reviewVM)
        {
            if (ModelState.IsValid)
            {
                // Map ReviewVM to Review entity
                var review = new Review
                {
                    UserId = reviewVM.UserId,
                    BookId = reviewVM.BookId,
                    Content = reviewVM.Content,
                    Rating = reviewVM.Rating
                };

                // Add the Review entity to the database context
                _context.Reviews.Add(review);
                _context.Books.Find(review.BookId).Rating = _context.Reviews.Where(r => r.BookId == review.BookId).Average(r => r.Rating);
                await _context.SaveChangesAsync();

                // Redirect back to the book's details page
                return RedirectToAction("Details", "Home", new { id = review.BookId });
            }

            // If ModelState is invalid, return the partial view with validation errors
            return PartialView("_CreateReview", reviewVM);
        }
        // POST: Reviews/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { bookId = review.BookId });
        }


    }
}
