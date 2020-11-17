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

        [HttpPost("Hobby/Edit/{HobbyId}")] // EDIT/UPDATE
        public RedirectToActionResult UpdateHobby(Hobby fromForm, int HobbyId)
        {
            fromForm.HobbyId = HobbyId;
            _context.Update(fromForm);
            _context.Entry(fromForm).Property("CreatedAt").IsModified = false;
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }

        [HttpGet("join/{HobbyId}")] //ADD/REMOVE RELATIONSHIP
        public RedirectToActionResult Join(int HobbyId)
        {
            int UserId = (int)HttpContext.Session.GetInt32("UserId");
            UserHobby HobbyList = _context.UserHobbies.Include(b => b.Hobbyist).FirstOrDefault(a => a.Hobbyist.UserId == UserId && a.Hobby.HobbyId == HobbyId);
            if (HobbyList == null)
            {
                UserHobby NewHobby = new UserHobby { };
                User ExistingUser = _context.Users.FirstOrDefault(u => u.UserId == UserId);
                Hobby ExistingHobby = _context.Hobbies.FirstOrDefault(w => w.HobbyId == HobbyId);
                NewHobby.Hobbyist = ExistingUser;
                NewHobby.Hobby = ExistingHobby;
                _context.Add(NewHobby);
            }
            _context.SaveChanges();
            return RedirectToAction("Dashboard", "Home");
        }

        [HttpPost("new")] //CREATE NEW/SAVE TO DB
        public IActionResult CreateHobby(Hobby fromForm)
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

                NewHobby.Hobby = ExistingHobby;
                _context.Add(NewHobby);
                _context.SaveChanges();
                int HobbyId = ExistingHobby.HobbyId;
                return RedirectToAction("Hobby", "Home", new { HobbyyId = HobbyId });
            }
            return View("NewHobby", Hobby);
        }
    }
}