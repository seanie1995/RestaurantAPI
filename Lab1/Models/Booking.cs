using System.ComponentModel.DataAnnotations;
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
        public int? FK_TableId { get; set; }

        [ForeignKey("Customer")]
        [Required]
        public int FK_CustomerId { get; set; }

		[ForeignKey(nameof(FK_CustomerId))]
		public virtual Customer Customer { get; set; }
		
        [ForeignKey(nameof(FK_TableId))]
		public virtual Table Table { get; set; }

	}
}
