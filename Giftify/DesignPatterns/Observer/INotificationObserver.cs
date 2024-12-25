public interface INotificationObserver
{
    Task OnNotificationReceived(INotification notification);
}
