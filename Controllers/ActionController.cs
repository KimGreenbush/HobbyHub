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
        public IActionResult UpdateHobby(Hobby fromForm, int HobbyId)
        {
            // Hobby toUpdate = _context.Hobbies.FirstOrDefault(w => w.HobbyId == HobbyId);
            if (ModelState.IsValid)
            {
                fromForm.HobbyId = HobbyId;
                _context.Update(fromForm);
                _context.Entry(fromForm).Property("CreatedAt").IsModified = false;
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Home");
            }
            return View("EditHobby", fromForm);
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
                _context.SaveChanges();
            }
            return RedirectToAction("Dashboard", "Home");
        }

        [HttpPost("Hobby/New")] //CREATE NEW/SAVE TO DB
        public IActionResult CreateHobby(Hobby fromForm)
        {
            Hobby Hobby = new Hobby {};
            if (ModelState.IsValid)
            {
                List<Hobby> ExistHobby = _context.Hobbies.Where(h => h.Name == fromForm.Name).ToList();
                if (ExistHobby.Count < 1)
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
                    return RedirectToAction("Hobby", "Home", new { HobbyId = HobbyId });
                }
                else
                {
                    ModelState.AddModelError("Name", "Name must be unique");
                    return View("NewHobby", fromForm);
                }
            }
            return View("NewHobby", Hobby);
        }
    }
}