using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMunch.Web.Data;
using MiniMunch.Web.Extensions;
using MiniMunch.Web.Models;
using MiniMunch.Web.Services;
using MiniMunch.Web.ViewModels;

namespace MiniMunch.Web.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _db;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;
    private readonly IWebHostEnvironment _environment;

    public AccountController(AppDbContext db, IJwtService jwtService, IPasswordService passwordService, IWebHostEnvironment environment)
    {
        _db = db;
        _jwtService = jwtService;
        _passwordService = passwordService;
        _environment = environment;
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
        {
            return View(model);
        }

        var email = model.Email.Trim().ToLowerInvariant();
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email && x.IsActive);

        if (user is null || !_passwordService.Verify(model.Password, user.PasswordHash))
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(model);
        }

        SignInWithJwt(user, model.RememberMe);
        TempData["Success"] = $"Welcome back, {user.FullName}!";

        if (!string.IsNullOrWhiteSpace(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        {
            return Redirect(model.ReturnUrl);
        }

        return user.Role == UserRole.Admin
            ? RedirectToAction("Dashboard", "Admin")
            : RedirectToAction("Index", "Menu");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var email = model.Email.Trim().ToLowerInvariant();
        var exists = await _db.Users.AnyAsync(x => x.Email.ToLower() == email);
        if (exists)
        {
            ModelState.AddModelError(nameof(model.Email), "This email is already registered.");
            return View(model);
        }

        var user = new AppUser
        {
            FullName = model.FullName.Trim(),
            Email = email,
            PasswordHash = _passwordService.Hash(model.Password),
            Role = UserRole.Customer,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Account created successfully. Please login to continue.";
        return RedirectToAction(nameof(Login));
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        Response.Cookies.Delete(JwtService.CookieName);
        TempData["Success"] = "You have been logged out.";
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var userId = User.GetUserId();
        var user = await _db.Users.FindAsync(userId);
        if (user is null)
        {
            return NotFound();
        }

        return View(new ProfileViewModel
        {
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            AddressLine = user.AddressLine,
            City = user.City,
            PinCode = user.PinCode
        });
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Profile(ProfileViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userId = User.GetUserId();
        var user = await _db.Users.FindAsync(userId);
        if (user is null)
        {
            return NotFound();
        }

        user.FullName = model.FullName.Trim();
        user.PhoneNumber = model.PhoneNumber;
        user.AddressLine = model.AddressLine;
        user.City = model.City;
        user.PinCode = model.PinCode;
        user.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        SignInWithJwt(user, rememberMe: true);
        TempData["Success"] = "Profile updated.";
        return RedirectToAction(nameof(Profile));
    }

    [HttpGet]
    public IActionResult AccessDenied() => View();

    private void SignInWithJwt(AppUser user, bool rememberMe)
    {
        var token = _jwtService.GenerateAccessToken(user);
        var expires = rememberMe ? DateTimeOffset.UtcNow.AddDays(7) : DateTimeOffset.UtcNow.AddHours(2);

        Response.Cookies.Append(JwtService.CookieName, token, new CookieOptions
        {
            HttpOnly = true,
            Secure = !_environment.IsDevelopment(),
            SameSite = SameSiteMode.Lax,
            Expires = expires
        });
    }
}
