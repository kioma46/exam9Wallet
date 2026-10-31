using exam9Wallet.DAL.Interfaces;
using exam9Wallet.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace exam9Wallet.BLL.Services;

public class UniqueCodeGeneratorService
{
    private readonly UserManager<User> _userManager;
    public UniqueCodeGeneratorService( UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<int> GenerateUniqueCodeAsync()
    {
        Random rnd = new Random();
        while (true)
        {
            int code = rnd.Next(100000, 1000000);
            if (!await _userManager.Users.AnyAsync(u => u.UniqueCode == code))
                return code;
        }
    }
}