using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Capacity { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
