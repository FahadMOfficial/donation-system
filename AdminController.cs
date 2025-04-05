using Microsoft.AspNetCore.Mvc;
using GiveAid.Models.ViewModels;

namespace GiveAid.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // For demo purposes, hardcoded admin credentials
                if (model.Email == "admin@give-aid.org" && model.Password == "admin123")
                {
                    // In a real application, you would set authentication cookies
                    return RedirectToAction("Index", "Dashboard");
                }

                ModelState.AddModelError("", "Invalid email or password");
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            // In a real application, you would clear authentication cookies
            return RedirectToAction("Login");
        }
    }
}