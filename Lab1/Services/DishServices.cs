using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Lab1.Models.DTOs;

namespace Lab1.Services
{
	public class DishServices : IDishServices
	{
		public readonly IDishRepo _dishRepo;

        public DishServices(IDishRepo dishRepo)
        {
            _dishRepo = dishRepo;
        }

        public async Task AddDishAsync(DishDTO newDish)
		{
			var dish = new Dish
			{
				Name = newDish.Name,
				Price = newDish.Price,
				Availability = newDish.Availabilty
			};

			await _dishRepo.AddDishAsync(dish);
		}

		public async Task DeleteDishAsync(int id)
		{
			var Dish = await _dishRepo.GetDishByIdAsync(id);

			await _dishRepo.DeleteDishAsync(id);
		}

		public async Task<IEnumerable<Dish>> GetAllDishesAsync()
		{
			var dishList = await _dishRepo.GetAllDishesAsync();

			return dishList;
		}

		public async Task<Dish> GetDishByIdAsync(int id)
		{
			var Dish = await _dishRepo.GetDishByIdAsync(id);

			return Dish;
		}

		public async Task UpdateDishAsync(int dishId, DishDTO updatedDIsh)
		{
			var existingDish = await _dishRepo.GetDishByIdAsync(dishId);

			var newDish = new Dish
			{
				Name = updatedDIsh.Name,
				Price = updatedDIsh.Price,
				Availability = updatedDIsh.Availabilty
			};

			await _dishRepo.UpdateDishAsync(existingDish, newDish);
		}
	}
}
