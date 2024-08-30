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
        public async Task AddDishAsync(Dish dish)
		{
			await _context.Dish.AddAsync(dish);
			_context.SaveChanges();
		}

		public async Task DeleteDishAsync(int id)
		{
			var dishToDelete = await _context.Dish.FindAsync(id);
			 _context.Dish.Remove(dishToDelete);
			_context.SaveChanges();
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

		public async Task UpdateDishAsync(Dish existingDish, Dish updatedDish)
		{
			
			if (existingDish != null)
			{
				existingDish.Name = updatedDish.Name;
				existingDish.Price = updatedDish.Price;
				existingDish.Availability = updatedDish.Availability;
			}

			await _context.SaveChangesAsync();
		}
	}
}
