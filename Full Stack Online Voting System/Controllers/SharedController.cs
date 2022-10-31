using Microsoft.AspNetCore.Mvc;

namespace Full_Stack_Online_Voting_System.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult Index()
        {
            if (ViewBag.username.length < 1)
            {
                ViewBag.username = "Guest";
            }
            return View();
        }
    }
}
