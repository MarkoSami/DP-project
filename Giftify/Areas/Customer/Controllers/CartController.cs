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
            Cart cart;
            if (!cartId.HasValue)
            {
                // Create a new empty cart if no cart exists
                cart = new Cart
                {
                    UserId = default,
                    CartItems = new List<CartItem>() // Initialize CartItems as an empty list
                };
            }
            else
            {
                // Retrieve the cart from the database using the CartId
                var existingCart = unitOfWork.Cart.Get(c => c.Id == cartId.Value, "CartItems,CartItems.Book");

                if (existingCart == null)
                {
                    // If no cart is found, create a new empty cart
                    cart = new Cart
                    {
                        UserId = default,
                        CartItems = new List<CartItem>()
                    };
                }
                else
                {
                    // If cart exists, use the fetched cart
                    cart = existingCart;
                }
            }

            return View(cart);
        }


        [HttpPost]
        public IActionResult AddToCart(int bookId)
        {
            // Retrieve CartId from session
            var cartIdString = HttpContext.Session.GetInt32("CartId");

            Cart cart;

            if (string.IsNullOrEmpty(cartIdString.ToString()))
            {
                // If no cart is found, create a new cart
                cart = new Cart
                {
                    UserId = default,
                    CartItems = new List<CartItem>()
                };

                // Save the new cart and set CartId in session
                unitOfWork.Cart.Add(cart);
                unitOfWork.Save();
                HttpContext.Session.SetInt32("CartId", cart.Id);
            }
            else
            {
                // Retrieve the existing cart
                var cartId = cartIdString;
                cart = unitOfWork.Cart.Get(c => c.Id == cartId, "CartItems");

                if (cart == null)
                {
                    // Create a new cart if none found
                    cart = new Cart
                    {
                        UserId = default,
                        CartItems = new List<CartItem>()
                    };
                }
            }

            // Check if the book is already in the cart
            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.BookId == bookId);
            if (existingCartItem != null)
            {
                // If the book already exists, increment the quantity
                existingCartItem.Quantity++;
            }
            else
            {
                // If the book does not exist in the cart, add a new item
                var book = unitOfWork.Book.Get(b => b.Id == bookId);
                if (book != null)
                {
                    cart.CartItems.Add(new CartItem
                    {
                        CartId = cart.Id,
                        BookId = bookId,
                        Quantity = 1,
                        Book = book
                    });
                }
            }

            // Save the changes
            unitOfWork.Save();

            // Redirect back to the cart page or show a confirmation message
            return RedirectToAction("Index", "Cart");
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
