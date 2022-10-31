using Microsoft.AspNetCore.Mvc;

namespace Full_Stack_Online_Voting_System.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.username = HttpContext.Session.GetString("username");
            if (ViewBag.username == null)
            {
                ViewBag.username = "Guest";
            }
            return View();
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Clear();
            ViewBag.username = null;
            return RedirectToAction("Index");
        }


    }
}
