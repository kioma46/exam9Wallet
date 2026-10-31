using exam9Wallet.BLL.Interfaces;
using System.Diagnostics;
using exam9Wallet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace exam9Wallet.Controllers;

public class HomeController : Controller
{
    private readonly IServiceProviderService _serviceProviderService;

    public HomeController(IServiceProviderService serviceProviderService)
    {
        _serviceProviderService = serviceProviderService;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Providers = await _serviceProviderService.GetAll();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}