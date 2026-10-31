using System.ComponentModel.DataAnnotations;

namespace exam9Wallet.BLL.DTOs;

public class TopUpRequestDto
{
    public int UniqueCode { get; set; }
    public decimal Amount { get; set; }
}