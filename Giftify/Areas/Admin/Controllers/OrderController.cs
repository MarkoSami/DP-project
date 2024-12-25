using Giftify.DAL.Repository;
using Giftify.DAL.Repository.Interfaces;
using Giftify.Notifications;
using Giftify.Properties;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
public class OrderController : Controller
{
    private readonly IUnitOfWork unitOfWork;
    private readonly NotificationService notificationService;

    public OrderController(IUnitOfWork unitOfWork, NotificationService notificationService)
    {
        this.unitOfWork = unitOfWork;
        this.notificationService = notificationService;
    }

    public IActionResult Index()
    {
        var orders = unitOfWork.Order.GetAll(includeProps: "User,OrderItems");
        return View(orders);
    }

    // Fix the route attribute here
    [HttpGet]
    public IActionResult Details(int id)  // Changed parameter name to match convention
    {
        var order = unitOfWork.Order.Get(
                o => o.Id == id,
                includeProps: "User,OrderItems,OrderItems.Book"
        );

        if (order == null)
        {
            return NotFound();
        }

        return View("OrderDetails",order);
    }
    [HttpPost]
    public async Task<IActionResult> Confirm(int id)
    {
        var order = unitOfWork.Order.Get(
            o => o.Id == id,
            includeProps: "User");

        if (order == null)
        {
            return NotFound();
        }

        if (order.Status != OrderStatus.Pending)
        {
            return BadRequest("Only pending orders can be confirmed.");
        }

        order.Status = OrderStatus.Confirmed;
        unitOfWork.Save();

        // Send notification
        var notification = new Notification
        {
            Subject = $"Order #{order.Id} Confirmed",
            Message = NotificationTemplates.OrderConfirmation(order.Id.ToString(), order.User.UserName),
            Recipient = order.User.Email
        };

        await notificationService.NotifyAsync(notification, "email");

        return RedirectToAction(nameof(Details), new { id = order.Id });
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(int id)
    {
        var order = unitOfWork.Order.Get(
            o => o.Id == id,
            includeProps: "User");

        if (order == null)
        {
            return NotFound();
        }

        if (order.Status != OrderStatus.Pending)
        {
            return BadRequest("Only pending orders can be cancelled.");
        }

        order.Status = OrderStatus.Cancelled;
        unitOfWork.Save();

        // Send notification
        var notification = new Notification
        {
            Subject = $"Order #{order.Id} Cancelled",
            Message = NotificationTemplates.OrderCancellation(order.Id.ToString(), order.User.UserName),
            Recipient = order.User.Email
        };

        await notificationService.NotifyAsync(notification, "email");

        return RedirectToAction(nameof(Details), new { id = order.Id });
    }

}