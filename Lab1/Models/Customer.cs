using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab1.Models
{
        public class Customer
        {
            [Key] 
            public int Id { get; set; }
            [Required]
            [StringLength(50)]
            public string FirstName { get; set; }
            [Required]
            [StringLength(50)]
            public string LastName { get; set; }
            [Required]
            public string Email { get; set; }
            public virtual ICollection<Booking>? Bookings { get; set; }

        }
}
