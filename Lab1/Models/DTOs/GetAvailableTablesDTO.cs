namespace Lab1.Models.DTOs
{
    public class GetAvailableTablesDTO
    {
        
        public int partySize {  get; set; }
        public DateTime bookingStart { get; set; }
        public DateTime bookingEnd { get; set; }
    }
}
