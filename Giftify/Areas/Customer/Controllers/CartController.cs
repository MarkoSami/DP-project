using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Giftify.Models;
using Giftify.DAL.Repository;
using Giftify.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Giftify.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // Display the cart items
        public async Task<IActionResult> Index()
        {
            // Retrieve CartId from session
            var cartId = HttpContext.Session.GetInt32("CartId");

            // Check if CartId exists
            if (!cartId.HasValue)
            {
                // Create a new empty cart if no cart exists
                var cart = new Cart
                {
                    UserId = default,
                    CartItems = new List<CartItem>()
                };
                return View(cart);
            }

            // Retrieve the cart from the database using the CartId
            ViewData["Title"] = "Cart";
            var existingCart = unitOfWork.Cart.Get(c => c.Id == cartId.Value, "CartItems");

            // If no cart is found, create a new empty cart
            if (existingCart == null)
            {
                existingCart = new Cart
                {
                    UserId = default,
                    CartItems = new List<CartItem>()
                };
            }

            return View(existingCart);
        }

        //Update cart items
        [HttpDelete]
        public  IActionResult DeleteCartItem(int itemId) { 
        
            // Retrieve CartId from session
            var cartIdString = HttpContext.Session.GetString("CartId");

            if (string.IsNullOrEmpty(cartIdString))
            {
                // Handle case where CartId is not found (e.g., no cart created)
                return RedirectToAction("Create", "Cart");
            }

            var cartId = int.Parse(cartIdString);

            var cart = unitOfWork.Cart.Get(c=> c.Id == cartId,"CartItems");
            cart.CartItems = cart.CartItems.Where(ci => ci.Id != itemId).ToList();

            unitOfWork.Save();

            // Redirect to the Index action to refresh the cart view
            return RedirectToAction(nameof(Index));
        }

        // Update the quantity of a specific item
        [HttpPatch]
        public IActionResult UpdateQuantity(int itemId, int count)
        {
            // Retrieve CartId from session
            var cartIdString = HttpContext.Session.GetString("CartId");

            if (string.IsNullOrEmpty(cartIdString))
            {
                // Handle case where CartId is not found (e.g., no cart created)
                return RedirectToAction("Create", "Cart");
            }

            var cartId = int.Parse(cartIdString);

            var cart = unitOfWork.Cart.Get(c => c.Id == cartId, "CartItems");

            cart.CartItems.FirstOrDefault(ci => ci.Id == itemId).Quantity = count;

            return RedirectToAction(nameof(Index));
        }

        // Continue to purchase
        [HttpPost]
        public IActionResult ContinueToPurchase()
        {
            // Example: Redirect to a checkout page or process the cart
            return RedirectToAction("Index", "Checkout");
        }
    }
}
