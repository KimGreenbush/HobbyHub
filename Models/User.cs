using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csharp_belt.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MinLength(2, ErrorMessage = "Firstame must be lat least 2 letters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2, ErrorMessage = "Lastame must be at least 2 letters")]
        public string LastName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Username must be at least 2 letters")]
        [MaxLength(15, ErrorMessage = "Username must be no more than 15 letters")]
        [Display(Name = "Userame")]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Passwrord must at least 8 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm PW")]
        public string Confirm { get; set; }

        public List<UserHobby> Hobbies { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}