namespace exam9Wallet.DAL.Models;

public class ServiceAccount
{
    public int Id { get; set; }
    
    public int ServiceProviderId { get; set; }
    public virtual ServiceProvider? ServiceProvider { get; set; }
    
    public int UserId { get; set; }
    public virtual User? User { get; set; }
    
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}