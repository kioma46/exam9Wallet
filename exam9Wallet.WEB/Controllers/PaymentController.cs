
using exam9Wallet.BLL.Interfaces;
using exam9Wallet.DAL.Data;
using exam9Wallet.DAL.Models;
using exam9Wallet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exam9Wallet.Controllers;

[Authorize]
public class PaymentController : Controller
{
    private readonly WalletMobileContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IServiceProviderService _serviceProviderService;
    private readonly IServiceAccountService _serviceAccountService;

    public PaymentController(
        WalletMobileContext context,
        UserManager<User> userManager,
        IServiceProviderService serviceProviderService,
        IServiceAccountService serviceAccountService)
    {
        _context = context;
        _userManager = userManager;
        _serviceProviderService = serviceProviderService;
        _serviceAccountService = serviceAccountService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var providers = await _serviceProviderService.GetAll();
        ViewBag.Providers = providers;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Pay([FromBody] PaymentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new { success = false, message = string.Join(" | ", errors) });
        }

        var userId = int.Parse(_userManager.GetUserId(User)!);
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return NotFound(new { success = false, message = "Пользователь не найден." });
        }

        if (user.Balance < model.Amount)
        {
            return BadRequest(new { success = false, message = "Недостаточно средств на балансе кошелька." });
        }

        var serviceAccount = await _context.ServiceAccounts
            .FirstOrDefaultAsync(sa => sa.ServiceProviderId == model.ServiceProviderId && sa.AccountNumber == model.AccountNumber);

        if (serviceAccount == null)
        {
            return NotFound(new { success = false, message = "Указанный реквизит (номер счета) у данного поставщика услуг не существует." });
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            user.Balance -= model.Amount;
            _context.Users.Update(user);

            serviceAccount.Balance += model.Amount;
            _context.ServiceAccounts.Update(serviceAccount);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Json(new { success = true, message = "Оплата успешно проведена!", newBalance = user.Balance });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return StatusCode(500, new { success = false, message = "Произошла ошибка при обработке платежа: " + ex.Message });
        }
    }
}