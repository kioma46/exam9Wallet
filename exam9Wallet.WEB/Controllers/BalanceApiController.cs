using exam9Wallet.DAL.Models;
using exam9Wallet.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        return BadRequest(new { success = false, message = string.Join(" ", errors) });
    }

    var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UniqueCode == model.UniqueCode);
    if (user == null)
        return BadRequest(new { success = false, message = "Личный счет с таким номером не найден." });

    user.Balance += model.Amount;
    var result = await _userManager.UpdateAsync(user);

    if (!result.Succeeded)
        return StatusCode(500, new { success = false, message = "Произошла ошибка при обновлении баланса." });

    return Ok(new { 
        success = true, 
        message = $"Счет №{user.UniqueCode} успешно пополнен на {model.Amount} сом." 
    });
}

[HttpPost("transfer")]
[Authorize]
public async Task<IActionResult> Transfer([FromBody] TransferViewModel model)
{
    if (!ModelState.IsValid)
        return BadRequest(new { success = false, message = "Некорректные данные формы." });

    var senderId = _userManager.GetUserId(User);
    if (senderId == null)
        return Unauthorized(new { success = false, message = "Пользователь не авторизован." });

    var sender = await _userManager.FindByIdAsync(senderId);
    if (sender == null)
        return BadRequest(new { success = false, message = "Отправитель не найден." });

    if (sender.UniqueCode == model.UniqueCode)
        return BadRequest(new { success = false, message = "Нельзя переводить средства на собственный счет." });

    if (sender.Balance < model.Amount)
        return BadRequest(new { success = false, message = "Недостаточно средств на балансе." });

    var recipient = await _userManager.Users.FirstOrDefaultAsync(u => u.UniqueCode == model.UniqueCode);
    if (recipient == null)
        return BadRequest(new { success = false, message = "Счет получателя не найден." });

    sender.Balance -= model.Amount;
    recipient.Balance += model.Amount;

    await _userManager.UpdateAsync(sender);
    await _userManager.UpdateAsync(recipient);

    return Ok(new { 
        success = true, 
        message = $"Перевод на сумму {model.Amount:N2} сом успешно выполнен. Текущий баланс: {sender.Balance:N2} сом." 
    });
}
}