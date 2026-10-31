using exam9Wallet.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace exam9Wallet.DAL.Data;

public class WalletMobileContext : IdentityDbContext<User, IdentityRole<int>, int>
{

    public WalletMobileContext(DbContextOptions<WalletMobileContext> options) : base(options)
    {}
}