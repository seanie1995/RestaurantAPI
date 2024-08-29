using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Lab1.Services
{
	public class TableServices : ITableServices
	{
		public readonly ITableRepo _tableRepo;

        public TableServices(ITableRepo tableRepo)
        {
            _tableRepo = tableRepo;
        }
        public async Task AddNewTableAsync(int capacity)
		{
			var newTable = new Table
			{
				Capacity = capacity,
			};

			await _tableRepo.AddNewTableAsync(newTable);
		}

		public async Task DeleteTableByIdAsync(int id)
		{
			var tableToDelete = await _tableRepo.GetTableByIdAsync(id);
			await _tableRepo.DeleteTableByIdAsync(id);
		}

		public async Task<IEnumerable<Table>> GetAllTablesAsync()
		{
			var listOfTables = await _tableRepo.GetAllTables();

			return listOfTables;
		}

		public async Task<Table> GetTableByIdAsync(int id)
		{
			var table = await _tableRepo.GetTableByIdAsync(id);
			return table;
		}

		public async Task UpdateTableAsync(int capacity, int id)
		{
			var existingTable = await _tableRepo.GetTableByIdAsync(id);

			await _tableRepo.UpdateTableAsync(existingTable, capacity);

		}

		public async Task<IEnumerable<Booking>> GetBookingsConnectedToTableByIdAsync(int tableId)
		{
			var bookingsList = await _tableRepo.GetBookingsConnectedToTableByIdAsync(tableId);

			return bookingsList;

		}
	}
}
