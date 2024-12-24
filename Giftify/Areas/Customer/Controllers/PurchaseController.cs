using Giftify.DAL.Repository;
using Giftify.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Giftify.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class PurchaseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Purchase()
        {
            var cartId = HttpContext.Session.GetInt32("CartId");
            if (cartId == null)
            {
                return RedirectToAction("/Index", "Login");
            }
            var cart = _unitOfWork.Cart.Get(c => c.Id == cartId, includeProps: "CartItems,CartItems.Book");
            if (cart == null)
            {
                return RedirectToAction("/Index", "Login");
            }

            var viewModel = new PurchaseVM
            {
                CartItems = cart.CartItems.ToList()
            };

            return View(viewModel);
        }
    }
}