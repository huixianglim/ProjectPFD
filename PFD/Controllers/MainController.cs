using Microsoft.AspNetCore.Mvc;

namespace PFD.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
