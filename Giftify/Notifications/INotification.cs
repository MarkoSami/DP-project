public interface INotification
{
    string Subject { get; set; }     
    string Message { get; set; }     
    string Recipient { get; set; }
    string Sender { get; set; }      
}