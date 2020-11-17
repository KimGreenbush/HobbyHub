using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Csharp_belt.Models;

namespace Csharp_belt.Controllers
{
    public class ActionController : Controller
    {
        private MyContext _context;
        public ActionController(MyContext context)
        {
            _context = context;
        }

        // CRUD OPS

        // [HttpGet("delete/{ActivityId}")] // DELETE ONE
        // public RedirectToActionResult Delete(int ActivityId)
        // {
        //     Activity toDelete = _context.Activities.FirstOrDefault(w => w.ActivityId == ActivityId);
        //     _context.Remove(toDelete);
        //     _context.SaveChanges();
        //     return RedirectToAction("Dashboard", "Home");
        // }

        [HttpGet("join/{HobbyId}")] //ADD/REMOVE RELATIONSHIP
        public RedirectToActionResult Join(int HobbyId)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UserId");
            UserHobbyy HobbyList = _context.UserHobbies.Include(b => b.Hobbyist).FirstOrDefault(a => a.Hobbyist.UserId == UserId && a.Hobby.HobbyId == ActivityId);
            if (HobbyList == null)
            {
                UserHobby NewHobby = new UserHobby { };
                User ExistingUser = _context.Users.FirstOrDefault(u => u.UserId == UserId);
                Hobby ExistingHobby = _context.Hobbies.FirstOrDefault(w => w.HobbyId == HobbyId);
                NewHobby.Hobbyist = ExistingUser;
                NewHobby.Hobby = ExistingActivity;
                _context.Add(NewHobby);
            }
            else
            {
                _context.Remove(HobbyList);
            }
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }

        [HttpPost("new")] //CREATE NEW/SAVE TO DB
        public IActionResult CreateHobbyy(Hobby fromForm)
        {
            Hobby Hobby = new Hobby { };
            if (ModelState.IsValid)
            {
                int UserId = (int)HttpContext.Session.GetInt32("UserId");

                User ExistingUser = _context.Users.FirstOrDefault(u => u.UserId == UserId);

                fromForm.Creator = ExistingUser;

                _context.Add(fromForm);
                _context.SaveChanges();

                UserHobby NewHobby = new UserHobby { };

                Hobby ExistingHobby = _context.Hobbies.FirstOrDefault(w => w.HobbyId == fromForm.HobbyId);

                NewHobby.Hobbyist = ExistingUser;

                NewHobbyy.Hobby = ExistingHobby;
                _context.Add(NewHobby);
                _context.SaveChanges();
                int HobbyId = ExistingHobby.HobbyId;
                return RedirectToAction("Hobby", "Home", new { HobbyyId = HobbyId });
            }
            return View("NewHobby", Hobby);
        }
    }
}