using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Csharp_belt.Models;

namespace Csharp_belt.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }


        [HttpGet("")]
        public RedirectToActionResult ToIndex()
        {
            return RedirectToAction("Index");
        }

        [HttpGet("signin")]
        public ViewResult Index()
        {
            RegLogWrapper FormWrapper = new RegLogWrapper { };
            return View("Index", FormWrapper);
        }

        [HttpGet("Hobby")] //dashboard route for geting all and displaying particular table values
        public IActionResult Dashboard()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId != null)
            {
                ViewBag.User = _context.Users.FirstOrDefault(a => a.UserId == (HttpContext.Session.GetInt32("UserId")));
                List<Hobby> Hobbies = _context.Hobbies.Include(a => a.Hobbyists).ThenInclude(b => b.Hobbyist).Include(a => a.Creator).ToList();
                return View("Home", Hobbies);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("Hobby/New")] //route displaying form
        public IActionResult NewHobby()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId != null)
            {
                Hobby Hobby = new Hobby { };
                return View("NewHobby", Hobby);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("Hobby/{HobbyId}")] // display one from table
        public IActionResult Hobby(int HobbyId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId != null)
            {
                ViewBag.UserId = (int)HttpContext.Session.GetInt32("UserId");
                Hobby Hobby = _context.Hobbies.Include(a => a.Hobbyists).ThenInclude(b => b.Hobbyist).Include(a => a.Creator).FirstOrDefault(w => w.HobbyId == HobbyId);
                return View("Hobby", Hobby);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("Hobby/Edit/{HobbyId}")] // edit/update form
        public IActionResult EditHobby(int HobbyId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId != null)
            {
                ViewBag.UserId = (int)HttpContext.Session.GetInt32("UserId");
                Hobby Hobby = _context.Hobbies.FirstOrDefault(w => w.HobbyId == HobbyId);
                return View("EditHobby", Hobby);
            }
            return RedirectToAction("Index");
        }
    }
}