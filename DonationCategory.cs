using System;
using System.ComponentModel.DataAnnotations;

namespace GiveAid.Models
{
    public class DonationCategory
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Total Raised")]
        [DataType(DataType.Currency)]
        public decimal TotalRaised { get; set; }

        public bool IsActive { get; set; } = true;
    }
}