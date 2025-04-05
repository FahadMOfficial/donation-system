using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GiveAid.Models;

namespace GiveAid.Controllers
{
    public class UsersController : Controller
    {
        // This would normally use dependency injection for data access
        // For demo purposes, we're using mock data

        private static List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "John Smith", Email = "john@example.com", Phone = "+1 (555) 123-4567", RegistrationDate = DateTime.Parse("2024-03-15"), IsActive = true },
            new User { Id = 2, Name = "Sarah Johnson", Email = "sarah@example.com", Phone = "+1 (555) 234-5678", RegistrationDate = DateTime.Parse("2024-03-12"), IsActive = true },
            new User { Id = 3, Name = "Michael Brown", Email = "michael@example.com", Phone = "+1 (555) 345-6789", RegistrationDate = DateTime.Parse("2024-03-10"), IsActive = false },
            new User { Id = 4, Name = "Emily Davis", Email = "emily@example.com", Phone = "+1 (555) 456-7890", RegistrationDate = DateTime.Parse("2024-03-08"), IsActive = true },
            new User { Id = 5, Name = "Robert Wilson", Email = "robert@example.com", Phone = "+1 (555) 567-8901", RegistrationDate = DateTime.Parse("2024-03-05"), IsActive = true },
            new User { Id = 6, Name = "Jennifer Lee", Email = "jennifer@example.com", Phone = "+1 (555) 678-9012", RegistrationDate = DateTime.Parse("2024-03-02"), IsActive = true },
            new User { Id = 7, Name = "David Miller", Email = "david@example.com", Phone = "+1 (555) 789-0123", RegistrationDate = DateTime.Parse("2024-02-28"), IsActive = false },
            new User { Id = 8, Name = "Lisa Anderson", Email = "lisa@example.com", Phone = "+1 (555) 890-1234", RegistrationDate = DateTime.Parse("2024-02-25"), IsActive = true }
        };

        public IActionResult Index()
        {
            return View(_users);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // In a real app, this would update the database
                var existingUser = _users.FirstOrDefault(u => u.Id == id);
                if (existingUser != null)
                {
                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;
                    existingUser.Phone = user.Phone;
                    existingUser.IsActive = user.IsActive;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}