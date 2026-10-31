using exam9Wallet.DAL.Models;

namespace exam9Wallet.ViewModels;

public class TransactionViewModel
{
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public decimal Amount { get; set; }
    
    public int SenderId { get; set; }
    public virtual User? Sender { get; set; }
    
    public int ReceiverId { get; set; }
    public virtual User? Receiver { get; set; }
}