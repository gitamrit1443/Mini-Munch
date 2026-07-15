namespace MiniMunch.Web.Models;

public enum OrderStatus
{
    Pending = 1,
    Confirmed = 2,
    Preparing = 3,
    OutForDelivery = 4,
    Delivered = 5,
    Cancelled = 6
}
