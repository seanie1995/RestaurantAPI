using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace Lab1.Models
{
    public class Dish
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public bool Availability { get; set; }

        public ICollection<Booking> Bookings { get; set; }
        
    }
}
