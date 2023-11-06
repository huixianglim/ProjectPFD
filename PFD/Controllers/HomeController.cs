using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace PFD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }




        public IActionResult Index()
        {

            
            // Create an instance of the model and set the property
            

            return View();

        }
        

        public IActionResult Login()
        {
            

            // Set the session variable

            return View();

        }
        [HttpPost]
        public ActionResult Login(IFormCollection formData)
        {
            // Read inputs from textboxes
            // Email address converted to lowercase
            string loginID = formData["memberlogin"].ToString().ToLower();
            string password = formData["memberpassword"].ToString();
            if (loginID == "huixiang@gmail.com" && password == "12345")
            {
                // Redirect user to the "StaffMain" view through an action
                return RedirectToAction("Index");
            }
            else
            {
                // Redirect user back to the index view through an action
                return RedirectToAction("Login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}