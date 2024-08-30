using Lab1.Models;

namespace Lab1.Data.Repos.IRepos
{
	public interface IDishRepo
	{
		// Create a new dish
		Task AddDishAsync(Dish dish);

		// Read (retrieve) a dish by its ID
		Task<Dish> GetDishByIdAsync(int id);

		// Read (retrieve) all dishes
		Task<IEnumerable<Dish>> GetAllDishesAsync();

		// Update an existing dish
		Task UpdateDishAsync(Dish existingDish, Dish updatedDish);

		// Delete a dish by its ID
		Task DeleteDishAsync(int id);
	}
}
