using exam9Wallet.BLL.Interfaces;
using exam9Wallet.DAL.Data;
using exam9Wallet.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace exam9Wallet.BLL.Services;

public class ServiceAccountService : IServiceAccountService
{
    private readonly WalletMobileContext _context;
    private readonly UserManager<User> _userManager;

    public ServiceAccountService(WalletMobileContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task CreateDefaultAccountsForUserAsync(int userId)
    {
        var providers = await _context.ServiceProviders.ToListAsync();

        foreach (var provider in providers)
        {
            var exists = await _context.ServiceAccounts
                .AnyAsync(sa => sa.UserId == userId && sa.ServiceProviderId == provider.Id);

            if (!exists)
            {
                var serviceAccount = new ServiceAccount
                {
                    ServiceProviderId = provider.Id,
                    UserId = userId,
                    AccountNumber = $"ACC-{userId}-{provider.Id}",
                    Balance = 0
                };

                await _context.ServiceAccounts.AddAsync(serviceAccount);
            }
        }

        await _context.SaveChangesAsync();
    }

    public async Task SeedAccountsForAllExistingUsersAsync()
    {
        var users = await _userManager.Users.ToListAsync();
        var providers = await _context.ServiceProviders.ToListAsync();

        foreach (var user in users)
        {
            foreach (var provider in providers)
            {
                var exists = await _context.ServiceAccounts
                    .AnyAsync(sa => sa.UserId == user.Id && sa.ServiceProviderId == provider.Id);

                if (!exists)
                {
                    var serviceAccount = new ServiceAccount
                    {
                        ServiceProviderId = provider.Id,
                        UserId = user.Id,
                        AccountNumber = $"ACC-{user.Id}-{provider.Id}",
                        Balance = 0
                    };

                    await _context.ServiceAccounts.AddAsync(serviceAccount);
                }
            }
        }

        await _context.SaveChangesAsync();
    }
    
    public async Task Add(ServiceAccount entity)
    {
        await _context.ServiceAccounts.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ServiceAccount entity)
    {
        _context.ServiceAccounts.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(ServiceAccount entity)
    {
        _context.ServiceAccounts.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var sp = _context.ServiceAccounts.FirstOrDefault(p => p.Id == id);
        if (sp != null)
        {
            _context.ServiceAccounts.Remove(sp);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Service not found");
        }
    }

    public async Task<ServiceAccount> GetById(int id)
    {
        ServiceAccount? sp = await _context.ServiceAccounts.FirstOrDefaultAsync(p => p.Id == id)!;
        if (sp != null)
            return sp;
        else
            throw new Exception("Service not found");
    }

    public async Task<IEnumerable<ServiceAccount>> GetAll()
    {
        return await _context.ServiceAccounts.ToListAsync();
    }
}