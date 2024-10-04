using Lab1.Models;

namespace Lab1.Data.Repos.IRepos
{
	public interface IDishRepo
	{
		
		Task<bool> AddDishAsync(Dish dish);

		
		Task<Dish> GetDishByIdAsync(int id);

		Task<IEnumerable<Dish>> GetAllDishesAsync();

		
		Task<bool> UpdateDishAsync(Dish existingDish, Dish updatedDish);

		
		Task<bool> DeleteDishAsync(int id);
	}
}
