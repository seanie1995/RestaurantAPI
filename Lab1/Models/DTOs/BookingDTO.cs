using System.ComponentModel.DataAnnotations;

namespace Lab1.Models.DTOs
{
    public class BookingDTO
    {
        public int Id { get; set; }
        public int PartySize { get; set; }
        
        public DateTime BookingStart { get; set; }
        
        public DateTime BookingEnd { get; set; }

        public int TableId { get; set; }

        public int CustomerId { get; set; }

    }
}
