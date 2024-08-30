using System.ComponentModel.DataAnnotations;

namespace Lab1.Models
{
    public class Table
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Capacity { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
