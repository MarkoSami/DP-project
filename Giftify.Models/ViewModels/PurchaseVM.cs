// PurchaseViewModel.cs
using Giftify.Models;

public class PurchaseVM
{
    public List<CartItem> CartItems { get; set; }
    public double Total => CartItems?.Sum(item => item.Book.Price * item.Quantity) ?? 0;
}