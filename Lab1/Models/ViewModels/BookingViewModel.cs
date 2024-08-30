using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab1.Models.ViewModels
{
	public class BookingViewModel
	{
		
		public int Id { get; set; }		
		public int PartySize { get; set; }		
		public DateTime BookingStart { get; set; }
		public DateTime BookingEnd { get; set; }
		public int CustomerId { get; set; }
		public int? TableId { get; set; }

		
	}
}
