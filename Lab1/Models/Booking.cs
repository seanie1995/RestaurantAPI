﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PartySize { get; set; }
        [Required]
        public DateTime BookingStart { get; set; }
        [Required]
        public DateTime BookingEnd { get; set; }

        [ForeignKey("Table")]
        [Required]
        public int FK_TableId { get; set; }

        [ForeignKey("Customer")]
        [Required]
        public int FK_CustomerId { get; set; }
        
        [ForeignKey("Dish")]
        public int? FK_DishId { get; set; }

        [Required]
        public Customer Customer { get; set; }
        [Required]
        public Table Table { get; set; }
        public ICollection<Dish> Dishes { get; set; }

    }
}
