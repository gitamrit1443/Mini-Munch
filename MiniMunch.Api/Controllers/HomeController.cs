using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniMunch.Web.Data;

namespace MiniMunch.Web.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;

    public HomeController(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _db.Products
            .Include(x => x.Category)
            .Where(x => x.IsAvailable)
            .OrderBy(x => x.SortOrder)
            .Take(6)
            .ToListAsync();

        return View(products);
    }

    public IActionResult About() => View();
    public IActionResult Error() => View();
}
