using System.ComponentModel.DataAnnotations;
using exam9Wallet.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace exam9Wallet.ViewModels;

public class TransferViewModel
{
    [Required(ErrorMessage = "Введите номер счета")]
    [Remote("ValidateAccountExists", "Validation", ErrorMessage = "Счет с таким номером не найден")]
    [Display(Name = "Номер счета")]
    public int UniqueCode { get; set; }

    [Required(ErrorMessage = "Введите сумму")]
    [Range(50, 1000001, ErrorMessage = "Сумма перевода должна быть от 50 до 1 000 001")]
    [Display(Name = "Сумма")]
    public decimal Amount { get; set; }
    
    public int FromUserId { get; set; }
    public User? UserFromFrom { get; set; }
}