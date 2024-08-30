using Lab1.Models;
using Lab1.Models.DTOs;

public interface IDishServices
{
	// Create a new dish
	Task AddDishAsync(DishDTO dish);

	// Read (retrieve) a dish by its ID
	Task<Dish> GetDishByIdAsync(int id);

	// Read (retrieve) all dishes
	Task<IEnumerable<Dish>> GetAllDishesAsync();

	// Update an existing dish
	Task UpdateDishAsync(int dishId, DishDTO updatedDish);

	// Delete a dish by its ID
	Task DeleteDishAsync(int id);
}