using System;
using System.ComponentModel.DataAnnotations;

namespace Csharp_belt.Models
{
    public class LoginUser
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string UserEmail { get; set; }

        [Required]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [DataType(DataType.Password)]

        public string UserPassword { get; set; }
    }
}