using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GiveAid.Models;

namespace GiveAid.Controllers
{
    public class NGOsController : Controller
    {
        // This would normally use dependency injection for data access
        // For demo purposes, we're using mock data

        private static List<NGO> _ngos = new List<NGO>
        {
            new NGO { Id = 1, OrganizationName = "Children First", FocusAreas = "Children, Education", Contact = "info@childrenfirst.org", RegistrationDate = DateTime.Parse("2024-01-15"), IsActive = true },
            new NGO { Id = 2, OrganizationName = "Education for All", FocusAreas = "Education, Youth", Contact = "contact@educationforall.org", RegistrationDate = DateTime.Parse("2024-01-10"), IsActive = true },
            new NGO { Id = 3, OrganizationName = "Helping Hands", FocusAreas = "Disabled, Elderly", Contact = "info@helpinghands.org", RegistrationDate = DateTime.Parse("2023-12-20"), IsActive = true },
            new NGO { Id = 4, OrganizationName = "Women Empowerment", FocusAreas = "Women, Education", Contact = "contact@womenempowerment.org", RegistrationDate = DateTime.Parse("2023-12-05"), IsActive = false },
            new NGO { Id = 5, OrganizationName = "Youth Forward", FocusAreas = "Youth, Education", Contact = "info@youthforward.org", RegistrationDate = DateTime.Parse("2023-11-28"), IsActive = true },
            new NGO { Id = 6, OrganizationName = "Care for Elderly", FocusAreas = "Elderly, Healthcare", Contact = "info@careforelderly.org", RegistrationDate = DateTime.Parse("2023-11-15"), IsActive = true }
        };

        public IActionResult Index()
        {
            return View(_ngos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NGO ngo)
        {
            if (ModelState.IsValid)
            {
                // In a real app, this would save to a database
                ngo.Id = _ngos.Count > 0 ? _ngos.Max(n => n.Id) + 1 : 1;
                _ngos.Add(ngo);
                return RedirectToAction(nameof(Index));
            }
            return View(ngo);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var ngo = _ngos.FirstOrDefault(n => n.Id == id);
            if (ngo == null)
            {
                return NotFound();
            }
            return View(ngo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NGO ngo)
        {
            if (id != ngo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // In a real app, this would update the database
                var existingNGO = _ngos.FirstOrDefault(n => n.Id == id);
                if (existingNGO != null)
                {
                    existingNGO.OrganizationName = ngo.OrganizationName;
                    existingNGO.FocusAreas = ngo.FocusAreas;
                    existingNGO.Contact = ngo.Contact;
                    existingNGO.Phone = ngo.Phone;
                    existingNGO.Website = ngo.Website;
                    existingNGO.Address = ngo.Address;
                    existingNGO.Description = ngo.Description;
                    existingNGO.IsActive = ngo.IsActive;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ngo);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var ngo = _ngos.FirstOrDefault(n => n.Id == id);
            if (ngo == null)
            {
                return NotFound();
            }
            return View(ngo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var ngo = _ngos.FirstOrDefault(n => n.Id == id);
            if (ngo != null)
            {
                _ngos.Remove(ngo);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}