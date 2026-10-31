using exam9Wallet.BLL.Interfaces;
using exam9Wallet.BLL.Services;
using exam9Wallet.DAL.Models;
using exam9Wallet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace exam9Wallet.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly UniqueCodeGeneratorService _uniqueCodeGeneratorService;
    private readonly IServiceAccountService _serviceAccountService;

    public AccountController(
        UserManager<User> userManager, 
        SignInManager<User> signInManager,
        UniqueCodeGeneratorService uniqueCodeGeneratorService,
        IServiceAccountService serviceAccountService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _uniqueCodeGeneratorService = uniqueCodeGeneratorService;
        _serviceAccountService = serviceAccountService;
    }
    
    private int CurrentUserId => int.Parse(_userManager.GetUserId(User)!);
    
    

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            model.DateOfBirth = DateTime.SpecifyKind(model.DateOfBirth, DateTimeKind.Utc);
            User user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                ProfilePictureUrl = model.ProfilePictureUrl,
                DateOfBirth = model.DateOfBirth,
                UniqueCode = await _uniqueCodeGeneratorService.GenerateUniqueCodeAsync(),
                Balance = 100000
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _serviceAccountService.CreateDefaultAccountsForUserAsync(user.Id);

                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Success", "Account", new { id = user.Id });
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var loginOrEmail = model.LoginOrEmail?.Trim();

        if (loginOrEmail != null)
        {
            User? user = await _userManager.FindByEmailAsync(loginOrEmail);

            if (user == null && !string.IsNullOrEmpty(loginOrEmail) && loginOrEmail.All(char.IsDigit))
            {
                int uniqueCode = int.Parse(loginOrEmail);
                user = _userManager.Users.FirstOrDefault(u => u.UniqueCode == uniqueCode);
            }

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login or password");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName!,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "User account is locked out.");
                return View(model);
            }
        }

        ModelState.AddModelError(string.Empty, "Invalid login or password");
        return View(model);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> LogOff()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet]
    [Authorize]
    public IActionResult Edit(int? userId)
    {
        if (userId != null)
        {
            if (userId != CurrentUserId)
                return Forbid();

            User? user = _userManager.Users.FirstOrDefault(u => u.Id == userId.Value);
            if (user != null)
            {
                UserEditViewModel uevm = new()
                {
                    Id = user.Id,
                    Email = user.Email!,
                    UserName = user.UserName,
                    ProfilePictureUrl = user.ProfilePictureUrl,
                    DateOfBirth = user.DateOfBirth,
                };
                return View(uevm);
            }
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Edit(UserEditViewModel uevm)
    {
        if (ModelState.IsValid)
        {
            User? user = _userManager.Users.FirstOrDefault(u => u.Id == uevm.Id);

            if (user != null)
            {
                uevm.DateOfBirth = DateTime.SpecifyKind(uevm.DateOfBirth, DateTimeKind.Utc);

                user.UserName = uevm.UserName;
                user.Email = uevm.Email;
                user.DateOfBirth = uevm.DateOfBirth;
                user.ProfilePictureUrl = uevm.ProfilePictureUrl;

                user.NormalizedUserName = _userManager.KeyNormalizer.NormalizeName(uevm.UserName);
                user.NormalizedEmail = _userManager.KeyNormalizer.NormalizeEmail(uevm.Email);

                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(uevm);
            }

            return NotFound();
        }

        return View(uevm);
    }

    [Authorize]
    public async Task<IActionResult> Details(int? id)
    {
        if (id != null)
        {
            User? user = await _userManager.FindByIdAsync(id.Value.ToString());
            
            if (user != null)
                return View(user);
            
            return NotFound();
        }
        return BadRequest(ModelState);
    }

    [Authorize]
    public async Task<IActionResult> Success(int? id)
    {
        if (id != null)
        {
            User? user = await _userManager.FindByIdAsync(id.Value.ToString());
            
            if (user != null)
                return View(user);
            
            return NotFound();
        }
        return BadRequest(ModelState);
    }
}