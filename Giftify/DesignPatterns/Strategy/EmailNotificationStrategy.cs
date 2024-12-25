using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

public class EmailNotificationStrategy : INotificationStrategy
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;

    public EmailNotificationStrategy(IConfiguration configuration)
    {
        _smtpServer = configuration["SmtpSettings:Server"];
        _smtpPort = int.Parse(configuration["SmtpSettings:Port"]);
        _smtpUsername = configuration["SmtpSettings:Username"];
        _smtpPassword = configuration["SmtpSettings:Password"];
    }

    public async Task SendAsync(INotification notification)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Giftify", _smtpUsername));
        message.To.Add(new MailboxAddress("", notification.Recipient));
        message.Subject = notification.Subject;

        message.Body = new TextPart("html")
        {
            Text = notification.Message
        };

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_smtpUsername, _smtpPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
