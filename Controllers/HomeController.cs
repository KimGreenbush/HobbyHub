using System;
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

        //CHANGE ALL TABLE/QUERY NAMES

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

        [HttpGet("home")] //dashboard route for geting all and displaying particular table values
        public IActionResult Dashboard()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId != null)
            {
                ViewBag.User = _context.Users.FirstOrDefault(a => a.UserId == (HttpContext.Session.GetInt32("UserId")));
                List<Activity> Activities = _context.Activities.Include(a => a.Participants).ThenInclude(b => b.Participant).Include(a => a.Creator).OrderBy(a => a.Date).ToList();
                return View("Home", Activities);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("new")] //edit  route displaying form
        public IActionResult NewActivity()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId != null)
            {
                Activity Activity = new Activity { };
                return View("NewActivity", Activity);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("activity/{ActivityId}")] // display one from table
        public IActionResult Activity(int ActivityId)
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId != null)
            {
                ViewBag.UserId = (int)HttpContext.Session.GetInt32("UserId");
                Activity Activity = _context.Activities.Include(a => a.Participants).ThenInclude(b => b.Participant).Include(a => a.Creator).FirstOrDefault(w => w.ActivityId == ActivityId);
                return View("Activity", Activity);
            }
            return RedirectToAction("Index");
        }
    }
}