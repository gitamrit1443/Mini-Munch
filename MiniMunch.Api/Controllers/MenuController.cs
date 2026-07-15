using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMunch.Web.Data;

namespace MiniMunch.Web.Controllers;

public class MenuController : Controller
{
    private readonly AppDbContext _db;

    public MenuController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index(string? q, Guid? categoryId)
    {
        var query = _db.Products.Include(x => x.Category).Where(x => x.IsAvailable).AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim().ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(term) || x.Description.ToLower().Contains(term) || x.Ingredients.ToLower().Contains(term));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(x => x.CategoryId == categoryId.Value);
        }

        ViewBag.Search = q;
        ViewBag.SelectedCategoryId = categoryId;
        ViewBag.Categories = await _db.Categories.OrderBy(x => x.SortOrder).ToListAsync();

        var products = await query.OrderBy(x => x.SortOrder).ToListAsync();
        return View(products);
    }

    public async Task<IActionResult> Details(string slug)
    {
        var product = await _db.Products
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Slug == slug && x.IsAvailable);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }
}
