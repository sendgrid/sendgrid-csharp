using Microsoft.AspNetCore.Mvc;

namespace Inbound.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}