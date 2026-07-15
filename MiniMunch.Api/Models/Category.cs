using System.ComponentModel.DataAnnotations;

namespace MiniMunch.Web.Models;

public class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(80)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(240)]
    public string? Description { get; set; }

    public int SortOrder { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
