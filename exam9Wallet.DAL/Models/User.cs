using Microsoft.AspNetCore.Identity;

namespace exam9Wallet.DAL.Models;

public class User : IdentityUser<int>
{
    public string ProfilePictureUrl { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int UniqueCode { get; set; }
    public decimal Balance { get; set; }
}