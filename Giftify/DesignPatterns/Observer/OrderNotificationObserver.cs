public class OrderNotificationObserver : INotificationObserver
{
    private readonly ILogger<OrderNotificationObserver> _logger;

    public OrderNotificationObserver(ILogger<OrderNotificationObserver> logger)
    {
        _logger = logger;
    }

    public async Task OnNotificationReceived(INotification notification)
    {
        _logger.LogInformation($"Notification sent to {notification.Recipient}: {notification.Subject}");
        // You could also store notifications in the database here
    }
}