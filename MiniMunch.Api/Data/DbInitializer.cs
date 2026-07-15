using Microsoft.EntityFrameworkCore;
using MiniMunch.Web.Models;

namespace MiniMunch.Web.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var db = serviceProvider.GetRequiredService<AppDbContext>();
        await db.Database.EnsureCreatedAsync();

        if (!await db.Users.AnyAsync())
        {
            db.Users.AddRange(
                new AppUser
                {
                    FullName = "Mini Munch Admin",
                    Email = "admin@minimunch.local",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                    Role = UserRole.Admin,
                    PhoneNumber = "9999999999",
                    City = "Delhi",
                    AddressLine = "Mini Munch HQ"
                },
                new AppUser
                {
                    FullName = "Demo Customer",
                    Email = "customer@minimunch.local",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Customer@123"),
                    Role = UserRole.Customer,
                    PhoneNumber = "8888888888",
                    City = "Delhi",
                    AddressLine = "221 Breakfast Street"
                }
            );
            await db.SaveChangesAsync();
        }

        if (!await db.Categories.AnyAsync())
        {
            var classic = new Category { Name = "Classic", Description = "Soft stacks with timeless toppings", SortOrder = 1 };
            var stuffed = new Category { Name = "Stuffed", Description = "Loaded pancakes with creamy centres", SortOrder = 2 };
            var healthy = new Category { Name = "Healthy", Description = "Balanced bowls and light stacks", SortOrder = 3 };
            var seasonal = new Category { Name = "Seasonal", Description = "Chef specials for limited runs", SortOrder = 4 };
            db.Categories.AddRange(classic, stuffed, healthy, seasonal);
            await db.SaveChangesAsync();

            db.Products.AddRange(
                new Product
                {
                    CategoryId = classic.Id,
                    Name = "Classic Cloud Stack",
                    Slug = "classic-cloud-stack",
                    Description = "Three fluffy buttermilk pancakes finished with maple butter, whipped cream and toasted crumbs.",
                    Ingredients = "Buttermilk batter, maple butter, whipped cream, toasted crumbs",
                    Badge = "Best Seller",
                    AccentColor = "amber",
                    Price = 189,
                    Calories = 510,
                    PrepTimeMinutes = 12,
                    SortOrder = 1
                },
                new Product
                {
                    CategoryId = stuffed.Id,
                    Name = "Nutella Lava Minis",
                    Slug = "nutella-lava-minis",
                    Description = "Mini pancake bites with a warm chocolate-hazelnut core and cocoa dust.",
                    Ingredients = "Mini pancakes, chocolate hazelnut spread, cocoa dust, berry drizzle",
                    Badge = "Kids Love It",
                    AccentColor = "rose",
                    Price = 229,
                    Calories = 620,
                    PrepTimeMinutes = 14,
                    SortOrder = 2
                },
                new Product
                {
                    CategoryId = healthy.Id,
                    Name = "Berry Yogurt Stack",
                    Slug = "berry-yogurt-stack",
                    Description = "Light oat pancakes layered with Greek yogurt, berries and honey citrus glaze.",
                    Ingredients = "Oat batter, Greek yogurt, strawberries, blueberries, honey",
                    Badge = "Light Choice",
                    AccentColor = "violet",
                    Price = 249,
                    Calories = 430,
                    PrepTimeMinutes = 11,
                    SortOrder = 3
                },
                new Product
                {
                    CategoryId = seasonal.Id,
                    Name = "Caramel Banana Crunch",
                    Slug = "caramel-banana-crunch",
                    Description = "Golden pancakes with banana coins, salted caramel and almond praline crunch.",
                    Ingredients = "Buttermilk pancakes, banana, salted caramel, almond praline",
                    Badge = "Chef Pick",
                    AccentColor = "orange",
                    Price = 259,
                    Calories = 590,
                    PrepTimeMinutes = 13,
                    SortOrder = 4
                },
                new Product
                {
                    CategoryId = classic.Id,
                    Name = "Blueberry Velvet Stack",
                    Slug = "blueberry-velvet-stack",
                    Description = "Velvety pancakes topped with blueberry compote, lemon cream and vanilla crumble.",
                    Ingredients = "Pancakes, blueberry compote, lemon cream, vanilla crumble",
                    Badge = "Premium",
                    AccentColor = "blue",
                    Price = 279,
                    Calories = 560,
                    PrepTimeMinutes = 14,
                    SortOrder = 5
                },
                new Product
                {
                    CategoryId = stuffed.Id,
                    Name = "Lotus Biscoff Minis",
                    Slug = "lotus-biscoff-minis",
                    Description = "Soft mini pancakes tossed in Biscoff sauce with cookie crumble and cream ribbons.",
                    Ingredients = "Mini pancakes, Biscoff sauce, cookie crumble, vanilla cream",
                    Badge = "New",
                    AccentColor = "yellow",
                    Price = 239,
                    Calories = 610,
                    PrepTimeMinutes = 12,
                    SortOrder = 6
                }
            );
            await db.SaveChangesAsync();
        }
    }
}
