using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.Models;
using Lab1.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Lab1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DishesController : ControllerBase
	{
		private readonly IDishServices _dishServices;

		public DishesController(IDishServices dishServices)
		{
			_dishServices = dishServices;
		}

		[HttpGet]
		[Route("getAllDishes")]

		public async Task<ActionResult<IEnumerable<Dish>>> GetAllDishes()
		{
			var dishList = await _dishServices.GetAllDishesAsync();

			return Ok(dishList);
		}

		[HttpGet]
		[Route("getDishById/{dishId}")]

		public async Task<ActionResult<Dish>> GetDishById(int dishId)
		{
			var dish = await _dishServices.GetDishByIdAsync(dishId);

			return Ok(dish);
		}

		[HttpPost]
		[Route("addNewDish")]
		public async Task<ActionResult> AddDish(DishDTO dish)
		{
			await _dishServices.AddDishAsync(dish);

			return Ok();
		}

		[Authorize]
		[HttpDelete]
		[Route("deleteDishById/{dishId}")]
		public async Task<ActionResult> DeleteDish(int dishId)
		{
			await _dishServices.DeleteDishAsync(dishId);
			return Ok();
		}

		[HttpPut]
		[Route("updateDishById/{dishId}")]

		public async Task<ActionResult> UpdateDish(int dishId, DishDTO newDish)
		{
            Console.WriteLine(newDish.Availability);
            await _dishServices.UpdateDishAsync(dishId, newDish);
			
            return Ok();
		}
	}
}
