using Full_Stack_Online_Voting_System.Model;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;
using System.Web;
using Microsoft.AspNetCore.Session;
using System.Security.Cryptography;

namespace Full_Stack_Online_Voting_System.Controllers
{
    public class LoginController : Controller
    {
        DBConnection _db = new DBConnection();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                return RedirectToAction("Index", "Vote");
            }
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(Student student)
        {
            student.StudentID = Convert.ToInt32(Request.Form["studentid"]);
            student.Password = Request.Form["password"];
            if (_db.AuthenticateUser(student) == 1)
            {
                TempData["username"] = Convert.ToString(student.StudentID);
                HttpContext.Session.SetString("username", Convert.ToString(student.StudentID));
                return RedirectToAction("Index", "Vote");
            } 
            else
            {
                ViewData["failed"] = "Username or Password is incorrect.";
            }
            return View("Index", "Login");
        }
    }
}
