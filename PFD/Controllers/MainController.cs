using Microsoft.AspNetCore.Mvc;

namespace PFD.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult ReturnToView(string categoryName)
        {
            return RedirectToAction("Index","Main");

        }

      
        public IActionResult PayNow()
        {
            return View();
        }
    }
}
