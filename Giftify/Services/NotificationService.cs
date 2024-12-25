public class NotificationService
{
    private readonly List<INotificationObserver> _observers = new();
    private readonly Dictionary<string, INotificationStrategy> _strategies;

    public NotificationService(IConfiguration configuration)
    {
        _strategies = new Dictionary<string, INotificationStrategy>
        {
            { "email", new EmailNotificationStrategy(configuration) }
            // Add more strategies here
        };
    }

    public void Subscribe(INotificationObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Unsubscribe(INotificationObserver observer)
    {
        _observers.Remove(observer);
    }

    public async Task NotifyAsync(INotification notification, string strategy = "email")
    {
        if (_strategies.TryGetValue(strategy, out var notificationStrategy))
        {
            await notificationStrategy.SendAsync(notification);

            foreach (var observer in _observers)
            {
                await observer.OnNotificationReceived(notification);
            }
        }
        else
        {
            throw new ArgumentException($"Strategy {strategy} not found");
        }
    }
}
