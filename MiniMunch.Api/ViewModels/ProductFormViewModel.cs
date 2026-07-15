using System.ComponentModel.DataAnnotations;

namespace MiniMunch.Web.ViewModels;

public class ProductFormViewModel
{
    public Guid? Id { get; set; }

    [Required]
    public Guid CategoryId { get; set; }

    [Required, MaxLength(140)]
    public string Name { get; set; } = string.Empty;

    [Required, MaxLength(800)]
    public string Description { get; set; } = string.Empty;

    [Required, MaxLength(600)]
    public string Ingredients { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Badge { get; set; }

    [MaxLength(40)]
    public string AccentColor { get; set; } = "amber";

    [Range(1, 10000)]
    public decimal Price { get; set; }

    [Range(1, 2000)]
    public int Calories { get; set; }

    [Range(1, 120)]
    public int PrepTimeMinutes { get; set; }

    public bool IsAvailable { get; set; } = true;

    [Range(0, 1000)]
    public int SortOrder { get; set; }
}
