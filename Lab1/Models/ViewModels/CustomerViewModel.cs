using System.ComponentModel.DataAnnotations;

namespace Lab1.Models.ViewModels
{
	public class CustomerViewModel
	{	
		public int Id { get; set; }
		
		public string FirstName { get; set; }
		
		public string LastName { get; set; }
		
		public string Email { get; set; }
		
	}
}
