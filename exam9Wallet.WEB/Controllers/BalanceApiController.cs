using exam9Wallet.DAL.Models;
using exam9Wallet.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exam9Wallet.Controllers;

[Route("api/balance")]
[ApiController]
public class BalanceApiController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public BalanceApiController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("topup")]
    public async Task<IActionResult> TopUp([FromBody] TopUpViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage);
            
            return BadRequest(new { success = false, message = string.Join(" ", errors) });
        }

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UniqueCode == model.UniqueCode);
        if (user == null)
        {
            return BadRequest(new { success = false, message = "Личный счет с таким номером не найден." });
        }

        user.Balance += model.Amount;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return StatusCode(500, new { success = false, message = "Произошла ошибка при обновлении баланса." });
        }

        return Ok(new { 
            success = true, 
            message = $"Счет №{user.UniqueCode} успешно пополнен на {model.Amount} сом." 
        });
    }
}