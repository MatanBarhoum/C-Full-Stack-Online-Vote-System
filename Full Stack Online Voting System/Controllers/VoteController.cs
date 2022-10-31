using Full_Stack_Online_Voting_System.Model;
using Microsoft.AspNetCore.Mvc;

namespace Full_Stack_Online_Voting_System.Controllers
{
    public class VoteController : Controller
    {
        private DBConnection _db = new DBConnection();
        public IActionResult Index()
        {
            try
            {
                ViewBag.username = HttpContext.Session.GetString("username");
                var studentID = Convert.ToInt32(ViewBag.username);
                if (_db.HasVoted(studentID) || ViewBag.username.Length < 1) // hasvoted == 1
                {
                    TempData["voted"] = "You Already Voted.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception err) { Console.WriteLine(err); }
            TempData["voted"] = "You Already Voted.";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]

        public IActionResult Index(int id)
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            var studentID = Convert.ToInt32(ViewBag.username);
            id = Convert.ToInt32(Request.Form["contender"]);
            if (_db.Vote(id, studentID) == 1)
            {
                TempData["voted"] = "Thank you for voting.";
                return RedirectToAction("Index", "Home");
            }
            TempData["failedvoted"] = "Something went wrong. Try again.";
            return View();
        }
    }
}
