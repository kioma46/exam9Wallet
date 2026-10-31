
using exam9Wallet.BLL.Interfaces;
using exam9Wallet.BLL.Services;
using exam9Wallet.DAL.Data;
using exam9Wallet.DAL.Interfaces;
using exam9Wallet.DAL.Models;
using exam9Wallet.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace exam9Wallet;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<WalletMobileContext>(options => options.UseNpgsql(connection))
            .AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<WalletMobileContext>();
        
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });
        
        builder.Services.AddScoped<UniqueCodeGeneratorService>();
        
        builder.Services.AddScoped<ITransactionService, TransactionService>();

        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        await app.RunAsync();
    }
}