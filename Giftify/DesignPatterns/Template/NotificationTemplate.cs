public static class NotificationTemplates
{
    public static string OrderConfirmation(string orderNumber, string userName)
    {
        return $@"
            <h2>Order Confirmation</h2>
            <p>Dear {userName},</p>
            <p>Your order #{orderNumber} has been confirmed.</p>
            <p>Thank you for shopping with us!</p>";
    }

    public static string OrderCancellation(string orderNumber, string userName)
    {
        return $@"
            <h2>Order Cancelled</h2>
            <p>Dear {userName},</p>
            <p>Your order #{orderNumber} has been cancelled.</p>
            <p>If you didn't request this cancellation, please contact support.</p>";
    }
}
