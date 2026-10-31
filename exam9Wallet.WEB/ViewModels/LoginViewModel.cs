using System.ComponentModel.DataAnnotations;

namespace exam9Wallet.ViewModels;

public class LoginViewModel

{
    [Required]
    [Display(Name = "Enter your 6-digit account number or Email")]
    public string LoginOrEmail { get; set; }
 
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Display(Name = "Remember?")]
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}