using System;
using System.ComponentModel.DataAnnotations;

namespace PunkAPI.Models
{
    public class InputValidation
    {
        [Required]
        public int id { get; set; }
        public String Username { get; set; }
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}