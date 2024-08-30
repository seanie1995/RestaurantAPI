using Lab1.Models;

namespace Lab1.Data.Repos.IRepos
{
	public interface IDishRepo
	{
		
		Task AddDishAsync(Dish dish);

		
		Task<Dish> GetDishByIdAsync(int id);

		Task<IEnumerable<Dish>> GetAllDishesAsync();

		
		Task UpdateDishAsync(Dish existingDish, Dish updatedDish);

		
		Task DeleteDishAsync(int id);
	}
}
