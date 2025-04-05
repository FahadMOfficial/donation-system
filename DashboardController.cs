using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GiveAid.Models;
using GiveAid.Models.ViewModels;
using GiveAid.Data;

namespace GiveAid.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;

            // Seed some data if the database is empty
            if (!_context.Donations.Any())
            {
                SeedInitialData();
            }
        }

        public IActionResult Index()
        {
            var viewModel = new DashboardViewModel
            {
                TotalDonations = _context.Donations.Sum(d => d.Amount),
                DonationGrowthRate = 12.5m,
                TotalUsers = _context.Users.Count(),
                UserGrowthRate = 8.2m,
                TotalNGOs = _context.NGOs.Count(),
                NGOGrowthRate = 4.6m,
                TotalPrograms = 38,
                ProgramGrowthRate = 2.3m,
                RecentDonations = _context.Donations.OrderByDescending(d => d.Date).Take(5).ToList(),
                UpcomingPrograms = GetUpcomingPrograms()
            };

            return View(viewModel);
        }

        private List<ProgramEvent> GetUpcomingPrograms()
        {
            return new List<ProgramEvent>
            {
                new ProgramEvent { Name = "Medical Camp", Category = "Healthcare", Date = DateTime.Parse("2024-03-28"), Status = "Confirmed" },
                new ProgramEvent { Name = "School Supplies Drive", Category = "Education", Date = DateTime.Parse("2024-04-05"), Status = "Planning" },
                new ProgramEvent { Name = "Inclusive Sports Day", Category = "Special Needs", Date = DateTime.Parse("2024-04-12"), Status = "In Progress" },
                new ProgramEvent { Name = "Women's Empowerment Workshop", Category = "Women", Date = DateTime.Parse("2024-04-18"), Status = "Confirmed" },
                new ProgramEvent { Name = "Youth Leadership Camp", Category = "Youth", Date = DateTime.Parse("2024-04-22"), Status = "Planning" }
            };
        }

        private void SeedInitialData()
        {
            try
            {
                // Add sample users
                _context.Users.AddRange(
                    new User { Name = "John Smith", Email = "john@example.com", Phone = "+1 (555) 123-4567", RegistrationDate = DateTime.Parse("2024-03-15"), IsActive = true },
                    new User { Name = "Sarah Johnson", Email = "sarah@example.com", Phone = "+1 (555) 234-5678", RegistrationDate = DateTime.Parse("2024-03-12"), IsActive = true },
                    new User { Name = "Michael Brown", Email = "michael@example.com", Phone = "+1 (555) 345-6789", RegistrationDate = DateTime.Parse("2024-03-10"), IsActive = false }
                );

                // Add sample NGOs
                _context.NGOs.AddRange(
                    new NGO { OrganizationName = "Children First", FocusAreas = "Children, Education", Contact = "info@childrenfirst.org", RegistrationDate = DateTime.Parse("2024-01-15"), IsActive = true },
                    new NGO { OrganizationName = "Education for All", FocusAreas = "Education, Youth", Contact = "contact@educationforall.org", RegistrationDate = DateTime.Parse("2024-01-10"), IsActive = true }
                );

                _context.SaveChanges();

                // Add sample donation categories
                var children = new DonationCategory { Name = "Children", Description = "Support for children's welfare and education", TotalRaised = 45250, IsActive = true };
                var education = new DonationCategory { Name = "Education", Description = "Help provide quality education to all", TotalRaised = 32800, IsActive = true };

                _context.DonationCategories.Add(children);
                _context.DonationCategories.Add(education);

                _context.SaveChanges();

                // Add sample donations
                _context.Donations.AddRange(
                    new Donation { Name = "John Smith", Cause = "Children", Amount = 250, Date = DateTime.Parse("2024-03-22"), DonationCategoryId = children.Id },
                    new Donation { Name = "Sarah Johnson", Cause = "Education", Amount = 500, Date = DateTime.Parse("2024-03-21"), DonationCategoryId = education.Id },
                    new Donation { Name = "Michael Brown", Cause = "Healthcare", Amount = 150, Date = DateTime.Parse("2024-03-20"), DonationCategoryId = children.Id },
                    new Donation { Name = "Emily Davis", Cause = "Elderly", Amount = 300, Date = DateTime.Parse("2024-03-19"), DonationCategoryId = education.Id },
                    new Donation { Name = "Robert Wilson", Cause = "Disabled", Amount = 200, Date = DateTime.Parse("2024-03-18"), DonationCategoryId = children.Id }
                );

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                System.Diagnostics.Debug.WriteLine($"Error seeding data: {ex.Message}");
            }
        }
    }
}

