using Lab1.Models;

namespace Lab1.Data.Repos.IRepos
{
	public interface IDishesRepo
	{
		// Create a new dish
		Task AddDish(Dish dish);

		// Read (retrieve) a dish by its ID
		Task<Dish> GetDishById(int id);

		// Read (retrieve) all dishes
		Task<IEnumerable<Dish>> GetAllDishes();

		// Update an existing dish
		Task UpdateDish(Dish dish);

		// Delete a dish by its ID
		Task DeleteDish(int id);
	}
}
