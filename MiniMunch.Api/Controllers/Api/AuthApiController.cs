using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMunch.Web.Data;
using MiniMunch.Web.Extensions;
using MiniMunch.Web.Models;
using MiniMunch.Web.Services;
using MiniMunch.Web.ViewModels;

namespace MiniMunch.Web.Controllers.Api;

[ApiController]
[Route("api/auth")]
public class AuthApiController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IJwtService _jwtService;
    private readonly IPasswordService _passwordService;

    public AuthApiController(AppDbContext db, IJwtService jwtService, IPasswordService passwordService)
    {
        _db = db;
        _jwtService = jwtService;
        _passwordService = passwordService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var email = model.Email.Trim().ToLowerInvariant();
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == email && x.IsActive);
        if (user is null || !_passwordService.Verify(model.Password, user.PasswordHash))
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }

        var token = _jwtService.GenerateAccessToken(user);
        return Ok(new
        {
            accessToken = token,
            expiresAt = _jwtService.GetAccessTokenExpiryUtc(),
            user = new { user.Id, user.FullName, user.Email, Role = user.Role.ToString() }
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var email = model.Email.Trim().ToLowerInvariant();
        if (await _db.Users.AnyAsync(x => x.Email.ToLower() == email))
        {
            return Conflict(new { message = "This email is already registered." });
        }

        var user = new AppUser
        {
            FullName = model.FullName.Trim(),
            Email = email,
            PasswordHash = _passwordService.Hash(model.Password),
            Role = UserRole.Customer
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        var token = _jwtService.GenerateAccessToken(user);
        return Ok(new
        {
            accessToken = token,
            expiresAt = _jwtService.GetAccessTokenExpiryUtc(),
            user = new { user.Id, user.FullName, user.Email, Role = user.Role.ToString() }
        });
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var user = await _db.Users.FindAsync(User.GetUserId());
        return user is null
            ? NotFound()
            : Ok(new { user.Id, user.FullName, user.Email, Role = user.Role.ToString() });
    }
}
