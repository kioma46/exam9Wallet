using exam9Wallet.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace exam9Wallet.DAL.Data;

public class WalletMobileContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<exam9Wallet.DAL.Models.Transaction> Transactions { get; set; } 
    public DbSet<ServiceProvider> ServiceProviders { get; set; } 
    public DbSet<ServiceAccount> ServiceAccounts { get; set; } 
    
    public WalletMobileContext(DbContextOptions<WalletMobileContext> options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.Entity<User>()
            .HasIndex(u => u.UniqueCode)
            .IsUnique();
        
        builder.Entity<ServiceProvider>().HasData(
            new ServiceProvider { Id = 1, Name = "Акнет" },
            new ServiceProvider { Id = 2, Name = "Газпром" },
            new ServiceProvider { Id = 3, Name = "Северэлектро" }
        );
    }
}