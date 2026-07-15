using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiniMunch.Web.Data;
using MiniMunch.Web.Extensions;
using MiniMunch.Web.Models;
using MiniMunch.Web.ViewModels;

namespace MiniMunch.Web.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly AppDbContext _db;

    public AdminController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Dashboard()
    {
        ViewBag.TotalOrders = await _db.Orders.CountAsync();
        ViewBag.PendingOrders = await _db.Orders.CountAsync(x => x.Status == OrderStatus.Pending || x.Status == OrderStatus.Confirmed || x.Status == OrderStatus.Preparing);
        ViewBag.TotalProducts = await _db.Products.CountAsync();
        ViewBag.TotalUsers = await _db.Users.CountAsync(x => x.Role == UserRole.Customer);
        ViewBag.Revenue = await _db.Orders.Where(x => x.Status != OrderStatus.Cancelled).SumAsync(x => x.TotalAmount);

        var latestOrders = await _db.Orders
            .Include(x => x.User)
            .OrderByDescending(x => x.CreatedAt)
            .Take(8)
            .ToListAsync();

        return View(latestOrders);
    }

    public async Task<IActionResult> Products()
    {
        var products = await _db.Products
            .Include(x => x.Category)
            .OrderBy(x => x.SortOrder)
            .ToListAsync();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        await PopulateCategoriesAsync();
        return View("ProductForm", new ProductFormViewModel { IsAvailable = true, PrepTimeMinutes = 12, Calories = 500 });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync();
            return View("ProductForm", model);
        }

        var slug = await MakeUniqueSlugAsync(model.Name);
        var product = new Product
        {
            CategoryId = model.CategoryId,
            Name = model.Name.Trim(),
            Slug = slug,
            Description = model.Description.Trim(),
            Ingredients = model.Ingredients.Trim(),
            Badge = model.Badge,
            AccentColor = model.AccentColor,
            Price = model.Price,
            Calories = model.Calories,
            PrepTimeMinutes = model.PrepTimeMinutes,
            IsAvailable = model.IsAvailable,
            SortOrder = model.SortOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Product created.";
        return RedirectToAction(nameof(Products));
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(Guid id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        await PopulateCategoriesAsync();
        return View("ProductForm", new ProductFormViewModel
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            Name = product.Name,
            Description = product.Description,
            Ingredients = product.Ingredients,
            Badge = product.Badge,
            AccentColor = product.AccentColor,
            Price = product.Price,
            Calories = product.Calories,
            PrepTimeMinutes = product.PrepTimeMinutes,
            IsAvailable = product.IsAvailable,
            SortOrder = product.SortOrder
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(ProductFormViewModel model)
    {
        if (model.Id is null)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync();
            return View("ProductForm", model);
        }

        var product = await _db.Products.FindAsync(model.Id.Value);
        if (product is null)
        {
            return NotFound();
        }

        product.CategoryId = model.CategoryId;
        product.Name = model.Name.Trim();
        product.Slug = await MakeUniqueSlugAsync(model.Name, product.Id);
        product.Description = model.Description.Trim();
        product.Ingredients = model.Ingredients.Trim();
        product.Badge = model.Badge;
        product.AccentColor = model.AccentColor;
        product.Price = model.Price;
        product.Calories = model.Calories;
        product.PrepTimeMinutes = model.PrepTimeMinutes;
        product.IsAvailable = model.IsAvailable;
        product.SortOrder = model.SortOrder;
        product.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        TempData["Success"] = "Product updated.";
        return RedirectToAction(nameof(Products));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product is not null)
        {
            product.IsAvailable = false;
            product.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync();
            TempData["Success"] = "Product hidden from menu.";
        }
        return RedirectToAction(nameof(Products));
    }

    public async Task<IActionResult> Orders()
    {
        var orders = await _db.Orders
            .Include(x => x.User)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        return View(orders);
    }

    public async Task<IActionResult> OrderDetails(Guid id)
    {
        var order = await _db.Orders
            .Include(x => x.User)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (order is null)
        {
            return NotFound();
        }
        return View(order);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, OrderStatus status)
    {
        var order = await _db.Orders.FindAsync(id);
        if (order is null)
        {
            return NotFound();
        }

        order.Status = status;
        if (status == OrderStatus.Delivered)
        {
            order.DeliveredAt = DateTime.UtcNow;
        }
        await _db.SaveChangesAsync();
        TempData["Success"] = "Order status updated.";
        return RedirectToAction(nameof(OrderDetails), new { id });
    }

    private async Task PopulateCategoriesAsync()
    {
        ViewBag.Categories = new SelectList(await _db.Categories.OrderBy(x => x.SortOrder).ToListAsync(), "Id", "Name");
    }

    private async Task<string> MakeUniqueSlugAsync(string name, Guid? productId = null)
    {
        var baseSlug = SlugHelper.ToSlug(name);
        var slug = baseSlug;
        var counter = 2;

        while (await _db.Products.AnyAsync(x => x.Slug == slug && (!productId.HasValue || x.Id != productId.Value)))
        {
            slug = $"{baseSlug}-{counter++}";
        }

        return slug;
    }
}
