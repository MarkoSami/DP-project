using Giftify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Giftify.DataAccess.Data; // Add this if you have a DbContext

namespace Giftify.Areas.Customer.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context; // Add this

        // Constructor dependency injection
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Cart Model { get; set; }

        public async Task<IActionResult> OnGet()
        {
            // Retrieve CartId from session
            var cartId = HttpContext.Session.GetInt32("CartId");

            // Check if CartId exists
            if (!cartId.HasValue)
            {
                // Create a new empty cart if no cart exists
                Model = new Models.Cart
                {
                    UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value, // Assuming you're using authentication
                    CartItems = new List<CartItem>()
                };
            }
            else
            {
                // Retrieve the cart from the database
                Model = await _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Book)
                    .FirstOrDefaultAsync(c => c.Id == cartId.Value);

                // If no cart found, create a new one
                if (Model == null)
                {
                    Model = new Models.Cart
                    {
                        UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                        CartItems = new List<CartItem>()
                    };
                }
            }

            return Page();
        }
    }
}