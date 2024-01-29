using Microsoft.AspNetCore.Mvc;
using PFD.DAL;
using Microsoft.AspNetCore.Http;

using PFD.Models;
using System.Text.Json;

namespace PFD.Controllers
{
    public class MainController : Controller
    {
        private ContactsDAL contactsDAL = new ContactsDAL();
        private TransactionDAL transactionDAL = new TransactionDAL();

        private FeedbackDAL feedbackDAL = new FeedbackDAL();

        public IActionResult Index()
        {
            var AccountString = HttpContext.Session.GetString("AccountObject");
            var AccountObject = JsonSerializer.Deserialize<Users>(AccountString);
            int userID = AccountObject.UserID;

            List<Transaction> transactions = transactionDAL.GetTransactions(userID);


            return View(transactions);
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

        

        [HttpPost]
        public IActionResult setTutorial(IFormCollection data)
        {

                Console.WriteLine("test");
                HttpContext.Session.SetString("Completed", "true");
                return RedirectToAction("Index", "Main");


        }

        

        public IActionResult feedback()
        {
            List<Feedback> feedbacks = feedbackDAL.GetFeedbacks();
            feedbackViewModel page = new feedbackViewModel();
            page.FeedbackList = feedbacks;
            return View(page);
        }

        [HttpPost]
        public IActionResult feedback(feedbackViewModel feedback)
        {

            bool check = feedbackDAL.Create(feedback.FormFeedback);
            Console.WriteLine(check);
            return RedirectToAction("feedback","Main");
        }
    }
}
