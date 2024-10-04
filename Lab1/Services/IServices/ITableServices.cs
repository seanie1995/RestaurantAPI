using Lab1.Models;
using Lab1.Models.ViewModels;
using Lab1.Result;

namespace Lab1.Services.IServices
{
	public interface ITableServices
	{
		Task<IEnumerable<Table>> GetAllTablesAsync();
		Task<Table> GetTableByIdAsync(int id);
		Task AddNewTableAsync(int capacity);
		Task<ServiceResult> DeleteTableByIdAsync(int id);
		Task<ServiceResult> UpdateTableAsync(int capacity, int id);
		Task<IEnumerable<BookingViewModel>> GetBookingsConnectedToTableByIdAsync(int tableId);

        Task<IEnumerable<Table>> GetAvailableTablesAsync(int partySize, DateTime bookingStart, DateTime bookendEnd);

    }
}
