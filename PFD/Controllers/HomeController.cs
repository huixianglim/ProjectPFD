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
        private CrosscheckDAL crossCheckContext = new CrosscheckDAL();


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }




        public IActionResult Index()
        {
            
            // Create an instance of the model and set the property
            if (HttpContext.Session.GetString("AccountObject") != null)
            {
                return RedirectToAction("Index", "Main");
            }

            return View();

        }

        [HttpGet]
        public IActionResult Login()
        {

            if (HttpContext.Session.GetString("AccountObject") != null)
            {
                return RedirectToAction("Index", "Main");
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
                    userContext.UpdateLastLoggedIn(user.UserID);
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

        [HttpPost]
        public ActionResult FaceID(IFormCollection form)
        {

            string id = form["face_verify"];

            Crosschecks? check = crossCheckContext.GetUserDetails(id);

            if (check != null)
            {
                Users details =userContext.GetDetails(check.user_id);

                Users? user = userContext.Login(details.Email, details.Password);
                if (user == null)
                {

                    TempData["Error"] = true;
                    return View();
                }
                else
                {
                    userContext.UpdateLastLoggedIn(user.UserID);
                    var jsonString = JsonSerializer.Serialize(user);
                    HttpContext.Session.SetString("AccountObject", jsonString);
                    //Redirect user back to the index view through an action
                    return RedirectToAction("Index", "Main");
                }
            }
            return View();
        }

    }
}