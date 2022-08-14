using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.EgyptianRecipes.Controllers;
[Authorize]
public class HomeController : Controller
{
    // GET
    [HttpGet("/")]
    public IActionResult Index()
    {
        return Content("hi");
    }
}