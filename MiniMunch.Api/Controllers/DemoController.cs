using Microsoft.AspNetCore.Mvc;

namespace MiniMunch.Web.Controllers;

public class DemoController : Controller
{
    public IActionResult Flow()
    {
        return View();
    }
}
