namespace exam9Wallet.DAL.Models;

public class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    
    public int? SenderId { get; set; }
    public virtual User? Sender { get; set; }
    
    public int ReceiverId { get; set; }
    public virtual User? Receiver { get; set; }
}