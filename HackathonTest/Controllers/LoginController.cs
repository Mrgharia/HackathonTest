using Microsoft.AspNetCore.Mvc;
using HackathonTest.Models;

namespace HackathonTest.Controllers
{
    public class LoginController : Controller
    {
        // GET: /Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // Hardcoded check — baad mein database se replace karna
            if (vm.Email == "admin@natgashub.com" && vm.Password == "Admin@123")
            {
                HttpContext.Session.SetString("UserEmail", vm.Email);
                HttpContext.Session.SetString("UserName", "Admin");
                return RedirectToAction("Index", "Nomination");
            }
            ModelState.AddModelError("", "Invalid email or password.");
            return View(vm);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }

}
