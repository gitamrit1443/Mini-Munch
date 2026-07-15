using System.ComponentModel.DataAnnotations;

namespace MiniMunch.Web.Models;

public class AppUser
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(120)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(180)]
    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.Customer;

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [MaxLength(220)]
    public string? AddressLine { get; set; }

    [MaxLength(80)]
    public string? City { get; set; }

    [MaxLength(12)]
    public string? PinCode { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
