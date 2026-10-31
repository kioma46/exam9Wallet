using exam9Wallet.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace exam9Wallet.DAL.Data;

public class WalletMobileContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public WalletMobileContext(DbContextOptions<WalletMobileContext> options) : base(options)
    {}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .HasIndex(u => u.UniqueCode)
            .IsUnique();
    }
}