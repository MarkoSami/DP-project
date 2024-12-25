public interface INotificationStrategy
{
    Task SendAsync(INotification notification);
}