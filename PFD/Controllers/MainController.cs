using Microsoft.AspNetCore.Mvc;
using PFD.DAL;
using PFD.Models;

namespace PFD.Controllers
{
    public class MainController : Controller
    {
        private ContactsDAL contactsDAL = new ContactsDAL();

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult ReturnToView(string categoryName)
        {
            return RedirectToAction("Index","Main");

        }

        [HttpPost]
        public IActionResult passTemp(IFormCollection? form)
        {
            if (form != null)
            {
                TempData["Success"] = form["tempData"].ToString();
            }
            return RedirectToAction("Index", "Main");
        }

        public IActionResult PayNow()
        {
            List<Contacts> contacts = contactsDAL.GetContacts(1);
            return View(contacts);
        }

        public IActionResult Details(int id) { 
              Contacts contact = contactsDAL.GetDetails(id);
            return View(contact);
        
        }
    }
}
