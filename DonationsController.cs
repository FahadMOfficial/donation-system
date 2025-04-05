using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GiveAid.Models;

namespace GiveAid.Controllers
{
    public class DonationsController : Controller
    {
        // This would normally use dependency injection for data access
        // For demo purposes, we're using mock data

        private static List<DonationCategory> _categories = new List<DonationCategory>
        {
            new DonationCategory { Id = 1, Name = "Children", Description = "Support for children's welfare and education", TotalRaised = 45250, IsActive = true },
            new DonationCategory { Id = 2, Name = "Education", Description = "Help provide quality education to all", TotalRaised = 32800, IsActive = true },
            new DonationCategory { Id = 3, Name = "Disabled", Description = "Support for people with disabilities", TotalRaised = 18450, IsActive = true },
            new DonationCategory { Id = 4, Name = "Women", Description = "Empowering women in communities", TotalRaised = 15200, IsActive = true },
            new DonationCategory { Id = 5, Name = "Youth", Description = "Programs for youth development", TotalRaised = 8750, IsActive = true },
            new DonationCategory { Id = 6, Name = "Elderly", Description = "Care and support for the elderly", TotalRaised = 4300, IsActive = true }
        };

        public IActionResult Index()
        {
            return View(_categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DonationCategory category)
        {
            if (ModelState.IsValid)
            {
                // In a real app, this would save to a database
                category.Id = _categories.Count > 0 ? _categories.Max(c => c.Id) + 1 : 1;
                _categories.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, DonationCategory category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // In a real app, this would update the database
                var existingCategory = _categories.FirstOrDefault(c => c.Id == id);
                if (existingCategory != null)
                {
                    existingCategory.Name = category.Name;
                    existingCategory.Description = category.Description;
                    existingCategory.IsActive = category.IsActive;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _categories.Remove(category);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}