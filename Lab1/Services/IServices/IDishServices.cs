using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Result;

public interface IDishServices
{
	// Create a new dish
	Task<ServiceResult> AddDishAsync(DishDTO dish);

	// Read (retrieve) a dish by its ID
	Task<Dish> GetDishByIdAsync(int id);

	// Read (retrieve) all dishes
	Task<IEnumerable<Dish>> GetAllDishesAsync();

	// Update an existing dish
	Task<ServiceResult> UpdateDishAsync(int dishId, DishDTO updatedDish);

	// Delete a dish by its ID
	Task<ServiceResult> DeleteDishAsync(int id);
}