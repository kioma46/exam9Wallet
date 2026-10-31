namespace exam9Wallet.DAL.Models;

public class ServiceProvider
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<ServiceAccount> ServiceAccounts { get; set; } = new List<ServiceAccount>();
}