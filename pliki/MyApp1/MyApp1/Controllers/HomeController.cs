using Microsoft.AspNetCore.Mvc;

namespace MyApp1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("get")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
