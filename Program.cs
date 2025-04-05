using System;
using System.ComponentModel.DataAnnotations;

namespace GiveAid.Models
{
    // Renamed to avoid conflict with Program.cs in the root
    public class ProgramEvent
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Description { get; set; }
    }
}