using System;
using System.ComponentModel.DataAnnotations;

namespace Csharp_belt.Models
{
    public class LoginUser
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
}