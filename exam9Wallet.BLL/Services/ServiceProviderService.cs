using exam9Wallet.BLL.Interfaces;
using exam9Wallet.DAL.Data;
using exam9Wallet.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace exam9Wallet.BLL.Services;

public class ServiceProviderService : IServiceProviderService
{
    private readonly WalletMobileContext _context;
    public ServiceProviderService(WalletMobileContext context)
    {
        _context = context;
    }
    public async Task Add(ServiceProvider entity)
    {
        await _context.ServiceProviders.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(ServiceProvider entity)
    {
        _context.ServiceProviders.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(ServiceProvider entity)
    {
        _context.ServiceProviders.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteById(int id)
    {
        var sp = _context.ServiceProviders.FirstOrDefault(p => p.Id == id);
        if (sp != null)
        {
            _context.ServiceProviders.Remove(sp);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Service not found");
        }
    }

    public async Task<ServiceProvider> GetById(int id)
    {
        ServiceProvider? sp = await _context.ServiceProviders.FirstOrDefaultAsync(p => p.Id == id)!;
        if (sp != null)
            return sp;
        else
            throw new Exception("Service not found");
    }

    public async Task<IEnumerable<ServiceProvider>> GetAll()
    {
        return await _context.ServiceProviders.ToListAsync();
    }
}