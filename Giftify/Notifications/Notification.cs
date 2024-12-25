namespace Giftify.Notifications
{
    public class Notification : INotification
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Recipient { get; set; }
        public string Sender { get; set; }
    }
}
