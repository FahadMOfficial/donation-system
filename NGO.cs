using System;
using System.ComponentModel.DataAnnotations;

namespace GiveAid.Models
{
    public class NGO
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Organization Name")]
        public string OrganizationName { get; set; }

        [Required]
        [Display(Name = "Focus Areas")]
        public string FocusAreas { get; set; }

        [Required]
        [EmailAddress]
        public string Contact { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Url]
        [Display(Name = "Website")]
        public string Website { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Description { get; set; }

        [Display(Name = "Registration Date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        public bool IsActive { get; set; } = true;
    }
}