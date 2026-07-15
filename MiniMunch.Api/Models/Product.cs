using System.ComponentModel.DataAnnotations;

namespace MiniMunch.Web.Models;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CategoryId { get; set; }

    [MaxLength(140)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(160)]
    public string Slug { get; set; } = string.Empty;

    [MaxLength(800)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(600)]
    public string Ingredients { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Badge { get; set; }

    [MaxLength(40)]
    public string AccentColor { get; set; } = "amber";

    public decimal Price { get; set; }
    public int Calories { get; set; }
    public int PrepTimeMinutes { get; set; }
    public bool IsAvailable { get; set; } = true;
    public int SortOrder { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Category Category { get; set; } = null!;
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
