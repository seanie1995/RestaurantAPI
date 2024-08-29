using Lab1.Models;
using Lab1.Models.ViewModels;

namespace Lab1.Services.IServices
{
	public interface ITableServices
	{
		Task<IEnumerable<Table>> GetAllTablesAsync();
		Task<Table> GetTableByIdAsync(int id);
		Task AddNewTableAsync(int capacity);
		Task DeleteTableByIdAsync(int id);

		Task UpdateTableAsync(int capacity, int id);

		Task<IEnumerable<BookingViewModel>> GetBookingsConnectedToTableByIdAsync(int tableId);
	}
}
