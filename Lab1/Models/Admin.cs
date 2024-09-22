﻿using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
