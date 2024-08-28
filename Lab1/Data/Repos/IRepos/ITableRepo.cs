using Lab1.Models;

namespace Lab1.Data.Repos.IRepos
{
	public interface ITableRepo
	{
		Task<IEnumerable<Table>> GetAllTables();
		Task<Table> GetTableByIdAsync(int id);
		Task AddNewTableAsync(Table table);
		Task DeleteTableByIdAsync(int id);
		Task UpdateTableAsync(Table table, int newCapacity);


		Task<IEnumerable<Table>> FindTableWithEnoughSeatsAsync(int partySize);

		Task<bool> FindAvailableTableTimeAsync(Booking booking);


	}
}
