using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Result;

namespace Lab1.Services
{
	public class DishServices : IDishServices
	{
		public readonly IDishRepo _dishRepo;

        public DishServices(IDishRepo dishRepo)
        {
            _dishRepo = dishRepo;
        }

        public async Task<ServiceResult> AddDishAsync(DishDTO newDish)
		{
			var dish = new Dish
			{
				Name = newDish.Name,
				Price = newDish.Price,
				Availability = newDish.Availability
			};

			await _dishRepo.AddDishAsync(dish);

			return new ServiceResult
			{
				Success = true,
				Message = "Dish Added"
			};
		}

		public async Task<ServiceResult> DeleteDishAsync(int id)
		{
			var Dish = await _dishRepo.GetDishByIdAsync(id);

			if (Dish == null)
			{
				return new ServiceResult
				{
					Success = false,
					Message = "Dish not found"
				};
			}

			await _dishRepo.DeleteDishAsync(id);
			return new ServiceResult
			{
				Success = true,
				Message = "Dish deleted"
			};
		}

		public async Task<IEnumerable<Dish>> GetAllDishesAsync()
		{
			var dishList = await _dishRepo.GetAllDishesAsync();

			return dishList;
		}

		public async Task<Dish> GetDishByIdAsync(int id)
		{
			var Dish = await _dishRepo.GetDishByIdAsync(id);

			if (Dish == null)
			{
				return null;
			}

			return Dish;
		}

		public async Task<ServiceResult> UpdateDishAsync(int dishId, DishDTO updatedDIsh)
		{
			var existingDish = await _dishRepo.GetDishByIdAsync(dishId);

			if (existingDish == null)
			{
				return new ServiceResult
				{
					Success = false,
					Message = "Dish not found"
				};
			}

			var newDish = new Dish
			{
				Name = updatedDIsh.Name,
				Price = updatedDIsh.Price,
				Availability = updatedDIsh.Availability
			};

			await _dishRepo.UpdateDishAsync(existingDish, newDish);

			return new ServiceResult
			{
				Success = true,
				Message = "Dish updated"
			};
		}
	}
}
