using exam9Wallet.DAL.Data;
using exam9Wallet.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace exam9Wallet.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly WalletMobileContext _context;
    public UserRepository(WalletMobileContext context)
    {
        _context = context;
    }

    public async Task<bool> UniqueCodeExists(int code)
    {
        return await _context.Users.AnyAsync(u => u.UniqueCode == code);
    }
}