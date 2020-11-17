using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Csharp_belt.Models
{
    public class UserHobby
    {
        [Key]
        public int UserHobbyId { get; set; }

        public int UserId { get; set; }
        public User Hobbyist { get; set; }

        public int HobbyId { get; set; }
        public Hobby Hobby { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}