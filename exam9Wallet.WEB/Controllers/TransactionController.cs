using exam9Wallet.BLL.Interfaces;
using exam9Wallet.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace exam9Wallet.Controllers;

[Authorize]
public class TransactionController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ITransactionService _transactionService;

    public TransactionController(UserManager<User> userManager, ITransactionService transactionService)
    {
        _userManager = userManager;
        _transactionService = transactionService;
    }
    
    public async Task<IActionResult> Index(DateTime? from, DateTime? to)
    {
        var userIdStr = _userManager.GetUserId(User);
        if (userIdStr == null || !int.TryParse(userIdStr, out int userId))
            return RedirectToAction("Login", "Account");
        
        var allTransactions = await _transactionService.GetAllSortedByDate(from, to);

        var userTransactions = allTransactions.Where(t => t.ReceiverId == userId || t.SenderId == userId)
            .OrderByDescending(t => t.Date).ToList();

        ViewData["CurrentUserId"] = userId;

        return View(userTransactions);
    }
}