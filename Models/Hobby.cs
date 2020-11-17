using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Csharp_belt.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public User Creator { get; set; }

        public List<UserHobby> Hobbyists { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}