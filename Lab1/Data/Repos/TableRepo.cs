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
			return await _context.Table
				.Where(u => u.Capacity >= partySize)
				.ToListAsync();
		}

		public async Task<bool> FindAvailableTableTimeAsync(Booking booking)
		{
			return !await _context.Booking
				.AnyAsync(b => b.FK_TableId == booking.FK_TableId && (booking.BookingStart < b.BookingEnd) && (booking.BookingEnd  > b.BookingStart));
		}
	}
}
