using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data.Repos
{
	public class TableRepo : ITableRepo
	{
		private readonly RestaurantContext _context;

        public TableRepo(RestaurantContext context)
        {
            _context = context;
        }
        public async Task AddNewTableAsync(Table table)
		{
			await _context.Table.AddAsync(table);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteTableByIdAsync(int id)
		{
			var table = await _context.Table.FindAsync(id);

			if (table != null)
			{
				_context.Table.Remove(table);
			}
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Table>> GetAllTables()
		{
			var tables = await _context.Table.ToListAsync();

			return tables;
		}

		public async Task<Table> GetTableByIdAsync(int id)
		{
			var table = await _context.Table.FindAsync(id);
			return table;
		}

		public async Task UpdateTableAsync(Table table, int newCapacity)
		{
			var existingTable = await GetTableByIdAsync(table.Id);
			if (existingTable != null)
			{
				existingTable.Capacity = newCapacity;
			}

			await _context.SaveChangesAsync();

		}

		public async Task<IEnumerable<Table>> FindTableWithEnoughSeatsAsync(int partySize)
		{
			var tableList = await _context.Table
				.Where(u => u.Capacity >= partySize)
				.ToListAsync();

			return tableList;
		}

		public async Task<IEnumerable<Booking>> GetBookingsConnectedToTableByIdAsync(int tableId)
		{
			var table = await _context.Table.FindAsync(tableId);

			var bookingsList = await _context.Booking
								 .Where(b => b.FK_TableId == tableId)
								 .ToListAsync();
			return bookingsList;
		}

		public async Task<IEnumerable<Table>> GetAvailableTablesAsync(int partySize, DateTime bookingStart, DateTime bookingEnd)
		{
			var availableTables = await _context.Table
				.Where(s => s.Capacity >= partySize)
				.Where(t => !t.Bookings.Any(b => bookingStart < b.BookingEnd && bookingEnd > b.BookingStart))
				.ToListAsync();

			return availableTables;
        }
    }
}
