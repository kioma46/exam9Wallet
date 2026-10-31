using System.ComponentModel.DataAnnotations;

namespace exam9Wallet.ViewModels;

public class PaymentViewModel
{
    [Required(ErrorMessage = "Выберите поставщика услуг")]
    public int ServiceProviderId { get; set; }

    [Required(ErrorMessage = "Введите номер счета или реквизит")]
    public string AccountNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите сумму для оплаты")]
    [Range(0.01, 1000000, ErrorMessage = "Сумма должна быть больше 0")]
    public decimal Amount { get; set; }
}