using Giftify.DAL.Repository;
using Giftify.DAL.Repository.Interfaces;
using Giftify.Models;
using Giftify.Properties;
using Microsoft.AspNetCore.Mvc;

namespace Giftify.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var cartId = HttpContext.Session.GetInt32("CartId");
            if (cartId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var cart = unitOfWork.Cart.Get(c => c.Id == cartId, includeProps: "CartItems,CartItems.Book");
            if (cart == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var orderItems = cart.CartItems.Select(ci => new OrderItem
            {
                BookId = ci.BookId,
                Quantity = ci.Quantity,
                Price = ci.Book.Price
            }).ToList();
            cart.CartItems = [];

            // Create a new order
            var order = new Order
            {

                OrderItems = orderItems,
                TotalPrice = orderItems.Sum(oi => oi.Price * oi.Quantity),
                UserId = cart.UserId
            };

            unitOfWork.Order.Add(order);
            unitOfWork.Cart.Update(cart);
            unitOfWork.Save();

            var viewModel = new PurchaseVM
            {
                CartItems = cart.CartItems.ToList(),
            };

            return View("OrderConfirmationView",viewModel);
        }
    }
}
