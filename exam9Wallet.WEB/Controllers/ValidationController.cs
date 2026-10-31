using exam9Wallet.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace exam9Wallet.Controllers;

public class ValidationController : Controller
{
    private readonly UserManager<User> _userManager;

    public ValidationController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public bool ValidatePhoneNumber(string PhoneNumber) => PhoneNumber.All(char.IsDigit);
    
    public bool ValidateEmail(string email, int? id)
    {
        if (id == null)
            return !_userManager.Users.Any(u => u.Email == email);
        
        return !_userManager.Users.Any(u => u.Email == email && u.Id != id.Value);
    }
    
    public bool ValidateUserName(string userName, int? id)
    {
        if (id == null)
            return !_userManager.Users.Any(u => u.UserName == userName);
        
        return !_userManager.Users.Any(u => u.UserName == userName && u.Id != id.Value);
    }
    
    public bool ValidatePassword(string Password) => Password.Length >= 8 && Password.Any(char.IsDigit) && Password.Any(char.IsUpper) && Password.Any(c => !char.IsLetter(c));

    public bool ValidateDateOfBirth(DateTime DateOfBirth) => DateOfBirth <= DateTime.UtcNow.AddYears(-18);
}