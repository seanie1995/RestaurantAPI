using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data.Repos
{
	public class DishRepo : IDishRepo
	{
		public readonly RestaurantContext _context;

        public DishRepo(RestaurantContext context)
        {
            _context = context;
        }
        public async Task<bool> AddDishAsync(Dish dish)
		{
			if (dish == null )
			{
				return false;
			}

			await _context.Dish.AddAsync(dish);
			_context.SaveChanges();
			return true;
		}

		public async Task<bool> DeleteDishAsync(int id)
		{
			var dishToDelete = await _context.Dish.FindAsync(id);

			if (dishToDelete == null)
			{
				return false;
			}

			 _context.Dish.Remove(dishToDelete);
			_context.SaveChanges();
			return true;
		}

		public async Task<IEnumerable<Dish>> GetAllDishesAsync()
		{
			var dishList = await _context.Dish.ToListAsync();
			return dishList;
		}

		public async Task<Dish> GetDishByIdAsync(int id)
		{
			var dish = await _context.Dish.FindAsync(id);
			return dish;
		}

		public async Task<bool> UpdateDishAsync(Dish existingDish, Dish updatedDish)
		{
			
			if (existingDish != null)
			{
				existingDish.Name = updatedDish.Name;
				existingDish.Price = updatedDish.Price;
				existingDish.Availability = updatedDish.Availability;
			}

			await _context.SaveChangesAsync();
			return true;
		}
	}
}
