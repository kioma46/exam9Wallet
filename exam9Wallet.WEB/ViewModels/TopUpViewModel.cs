using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace exam9Wallet.ViewModels;

public class TopUpViewModel
{
    [Required(ErrorMessage = "Введите номер счета")]
    [Remote("ValidateAccountExists", "Validation", ErrorMessage = "Счет с таким номером не найден")]
    [Display(Name = "Номер счета")]
    public int UniqueCode { get; set; }

    [Required(ErrorMessage = "Введите сумму")]
    [Range(50, 50000, ErrorMessage = "Сумма пополнения должна быть от 50 до 50000")]
    [Display(Name = "Сумма")]
    public decimal Amount { get; set; }
}