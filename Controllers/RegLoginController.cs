using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Csharp_belt.Models;

namespace Csharp_belt.Controllers
{
    public class RegLoginController : Controller
    {
        private MyContext _context;
        public RegLoginController(MyContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(User fromForm)
        {
            RegLogWrapper FormWrapper = new RegLogWrapper { };
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Username == fromForm.Username)) //u.____/fromForm.____ is based on input field
                {
                    ModelState.AddModelError("Username", "Username taken");
                    return View("../Home/Index", FormWrapper);
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    fromForm.Password = Hasher.HashPassword(fromForm, fromForm.Password);
                    _context.Add(fromForm);
                    _context.SaveChanges();
                    HttpContext.Session.SetInt32("UserId", fromForm.UserId);
                    return RedirectToAction("Dashboard", "Home");
                }
            }
            return View("../Home/Index", FormWrapper);
        }

        [HttpPost("login")]
        public IActionResult LoginUser(LoginUser fromForm)
        {
            RegLogWrapper FormWrapper = new RegLogWrapper { };
            if (ModelState.IsValid)
            {
                User ExistingUser = _context.Users.FirstOrDefault(u => u.Username == fromForm.LoginUsername);
                if (ExistingUser == null)
                {
                    ModelState.AddModelError("LoginUsername", "Invalid Username");
                    return View("../Home/Index", FormWrapper);
                }
                PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
                PasswordVerificationResult result = Hasher.VerifyHashedPassword(fromForm, ExistingUser.Password, fromForm.LoginPassword);
                if (result == 0)
                {
                    ModelState.AddModelError("LoginEmail", "Password error");
                    return View("../Home/Index", FormWrapper);
                }
                HttpContext.Session.SetInt32("UserId", ExistingUser.UserId);
                return RedirectToAction("Dashboard", "Home");
            }
            return View("../Home/Index", FormWrapper);
        }

        [HttpGet("logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}