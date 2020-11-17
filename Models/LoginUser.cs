using System;
using System.ComponentModel.DataAnnotations;

namespace Csharp_belt.Models
{
    public class LoginUser
    {
        [Required]
        [Display(Name = "Username")]
        public string LoginUsername { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }
    }
}