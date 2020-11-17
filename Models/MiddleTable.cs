using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Csharp_belt.Models
{
    public class UserOtherRel
    {
        //CHANGE ALL NON USER NAMES

        [Key]
        public int UserOtherRelId { get; set; }

        public int UserId { get; set; }
        public User Participant { get; set; }

        public int OtherRelId { get; set; }
        public Activity OtherRel { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}