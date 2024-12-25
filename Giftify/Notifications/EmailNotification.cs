namespace Giftify.Notifications
{
    public class EmailNotification : INotification
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Recipient { get; set; }
        public string Sender { get; set; }
    }
}
