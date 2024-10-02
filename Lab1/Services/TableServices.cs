using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Lab1.Models.ViewModels;
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

			if (tableToDelete == null)
			{
				throw new Exception($"Table with ID: {id} not found");
			}

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

			if (table == null)
			{
				throw new Exception($"Table with ID: {id} not found");
			}

			return table;
		}

		public async Task UpdateTableAsync(int capacity, int id)
		{
			var existingTable = await _tableRepo.GetTableByIdAsync(id);

			if (existingTable == null) 
			{
				throw new Exception($"Table with ID: {id} not found.");
			}

			await _tableRepo.UpdateTableAsync(existingTable, capacity);

		}

		public async Task<IEnumerable<BookingViewModel>> GetBookingsConnectedToTableByIdAsync(int tableId)
		{
			var existingTable = await _tableRepo.GetTableByIdAsync(tableId);

			if (existingTable == null)
			{
				throw new Exception($"Table with ID: {tableId} not found");
			}

			var bookingsList = await _tableRepo.GetBookingsConnectedToTableByIdAsync(tableId);

			var bookingViewModelList = bookingsList.Select(b => new BookingViewModel
			{
				Id = b.Id,
				BookingStart = b.BookingStart,
				BookingEnd = b.BookingEnd,
				CustomerId = b.FK_CustomerId,
				PartySize = b.PartySize,
				TableId = b.FK_TableId
				
			}).ToList();

			return bookingViewModelList;
		}

		public async Task<IEnumerable<Table>> GetAvailableTablesAsync(int partySize, DateTime bookingStart, DateTime bookendEnd)
		{
			var availableTables = await _tableRepo.GetAvailableTablesAsync(partySize, bookingStart, bookendEnd);

			if (availableTables == null)
			{
				throw new Exception("No available tables");
			}

			return availableTables;
		}

    }
}
