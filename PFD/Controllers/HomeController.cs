using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFD.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using PFD.DAL;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Text.Json;


namespace PFD.Controllers
{
    public class HomeController : Controller
    {
        private UsersDAL userContext = new UsersDAL();
        private readonly ILogger<HomeController> _logger;
        


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }




        public IActionResult Index()
        {
            
            // Create an instance of the model and set the property
            if (HttpContext.Session.GetString("AccountObject") != null)
            {
                return RedirectToAction("Main", "Index");
            }

            return View();

        }

        [HttpGet]
        public IActionResult Login()
        {

            if (HttpContext.Session.GetString("AccountObject") != null)
            {
                return RedirectToAction("Main", "Index");
            }

            // Set the session variable

            return View();

        }


        [HttpPost]
        public ActionResult Login(IFormCollection formData)
        {
            
            //Read inputs from textboxes
            //Email address converted to lowercase
            string? UserID = formData["memberlogin"].ToString().ToLower();
            string? password = formData["memberpassword"].ToString();
            Console.WriteLine(UserID);
            Console.WriteLine(password);
            if (UserID != null && password != null){
                Users? user = userContext.Login(UserID, password);
                if (user == null)

                {


                    TempData["Error"] = true;
                    return View();
                }
                else
                {
                    var jsonString = JsonSerializer.Serialize(user);
                    HttpContext.Session.SetString("AccountObject", jsonString);
                    //Redirect user back to the index view through an action
                    return RedirectToAction("Index", "Main");
                }
            }
            else
            {
                return View();
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

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}