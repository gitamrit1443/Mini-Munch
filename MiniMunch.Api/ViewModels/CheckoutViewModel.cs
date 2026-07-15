using System.ComponentModel.DataAnnotations;

namespace MiniMunch.Web.ViewModels;

public class CheckoutViewModel
{
    [Required, MaxLength(120)]
    public string CustomerName { get; set; } = string.Empty;

    [Required, Phone, MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required, MaxLength(280)]
    public string DeliveryAddress { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Notes { get; set; }
}
