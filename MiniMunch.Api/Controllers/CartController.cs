using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMunch.Web.Data;
using MiniMunch.Web.Extensions;
using MiniMunch.Web.Models;
using MiniMunch.Web.ViewModels;

namespace MiniMunch.Web.Controllers;

[Authorize]
public class CartController : Controller
{
    private const decimal DeliveryFee = 39m;
    private readonly AppDbContext _db;

    public CartController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var items = await GetCartItemsAsync();
        ViewBag.SubTotal = items.Sum(x => x.UnitPriceSnapshot * x.Quantity);
        ViewBag.DeliveryFee = items.Any() ? DeliveryFee : 0m;
        return View(items);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(Guid productId, int quantity = 1, string? returnUrl = null)
    {
        quantity = Math.Clamp(quantity, 1, 10);
        var userId = User.GetUserId();
        var product = await _db.Products.FirstOrDefaultAsync(x => x.Id == productId && x.IsAvailable);
        if (product is null)
        {
            return NotFound();
        }

        var item = await _db.CartItems.FirstOrDefaultAsync(x => x.UserId == userId && x.ProductId == productId);
        if (item is null)
        {
            _db.CartItems.Add(new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
                UnitPriceSnapshot = product.Price
            });
        }
        else
        {
            item.Quantity = Math.Clamp(item.Quantity + quantity, 1, 10);
            item.UnitPriceSnapshot = product.Price;
            item.UpdatedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();
        TempData["Success"] = $"{product.Name} added to cart.";

        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Guid id, int quantity)
    {
        var userId = User.GetUserId();
        var item = await _db.CartItems.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (item is null)
        {
            return NotFound();
        }

        if (quantity <= 0)
        {
            _db.CartItems.Remove(item);
        }
        else
        {
            item.Quantity = Math.Clamp(quantity, 1, 10);
            item.UpdatedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Remove(Guid id)
    {
        var userId = User.GetUserId();
        var item = await _db.CartItems.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        if (item is not null)
        {
            _db.CartItems.Remove(item);
            await _db.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        var items = await GetCartItemsAsync();
        if (!items.Any())
        {
            TempData["Error"] = "Your cart is empty.";
            return RedirectToAction(nameof(Index));
        }

        var user = await _db.Users.FindAsync(User.GetUserId());
        var model = new CheckoutViewModel
        {
            CustomerName = user?.FullName ?? string.Empty,
            PhoneNumber = user?.PhoneNumber ?? string.Empty,
            DeliveryAddress = string.Join(", ", new[] { user?.AddressLine, user?.City, user?.PinCode }.Where(x => !string.IsNullOrWhiteSpace(x)))
        };
        ViewBag.Items = items;
        ViewBag.SubTotal = items.Sum(x => x.UnitPriceSnapshot * x.Quantity);
        ViewBag.DeliveryFee = DeliveryFee;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        var items = await GetCartItemsAsync();
        if (!items.Any())
        {
            TempData["Error"] = "Your cart is empty.";
            return RedirectToAction(nameof(Index));
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Items = items;
            ViewBag.SubTotal = items.Sum(x => x.UnitPriceSnapshot * x.Quantity);
            ViewBag.DeliveryFee = DeliveryFee;
            return View(model);
        }

        var subTotal = items.Sum(x => x.UnitPriceSnapshot * x.Quantity);
        var order = new Order
        {
            UserId = User.GetUserId(),
            OrderNumber = $"MM-{DateTime.UtcNow:yyyyMMddHHmmss}",
            SubTotal = subTotal,
            DeliveryFee = DeliveryFee,
            TotalAmount = subTotal + DeliveryFee,
            CustomerName = model.CustomerName.Trim(),
            PhoneNumber = model.PhoneNumber.Trim(),
            DeliveryAddress = model.DeliveryAddress.Trim(),
            Notes = model.Notes,
            Status = OrderStatus.Pending
        };

        foreach (var item in items)
        {
            order.Items.Add(new OrderItem
            {
                ProductId = item.ProductId,
                ProductName = item.Product.Name,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPriceSnapshot,
                LineTotal = item.UnitPriceSnapshot * item.Quantity
            });
        }

        _db.Orders.Add(order);
        _db.CartItems.RemoveRange(items);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Order placed successfully. Your order is now visible in order history.";
        return RedirectToAction("Index", "Orders");
    }

    private Task<List<CartItem>> GetCartItemsAsync()
    {
        var userId = User.GetUserId();
        return _db.CartItems
            .Include(x => x.Product)
            .ThenInclude(x => x.Category)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}
