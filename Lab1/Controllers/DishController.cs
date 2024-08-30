using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab1.Models;
using Lab1.Models.DTOs;

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
		[Route("getalldishes")]

		public async Task<ActionResult<IEnumerable<Dish>>> GetAllDishes()
		{
			var dishList = await _dishServices.GetAllDishesAsync();

			return Ok(dishList);
		}

		[HttpGet]
		[Route("getdishbyid/{dishId}")]

		public async Task<ActionResult<Dish>> GetDishById(int dishId)
		{
			var dish = await _dishServices.GetDishByIdAsync(dishId);

			return Ok(dish);
		}

		[HttpPost]
		[Route("addnewdish")]
		public async Task<ActionResult> AddDish(DishDTO dish)
		{
			await _dishServices.AddDishAsync(dish);

			return Ok();
		}

		[HttpDelete]
		[Route("deletedishbyid")]
		public async Task<ActionResult> DeleteDish(int dishId)
		{
			await _dishServices.DeleteDishAsync(dishId);
			return Ok();
		}

		[HttpPatch]
		[Route("updatedish")]

		public async Task<ActionResult> UpdateDish(int dishToUpdate, DishDTO newDish)
		{
			await _dishServices.UpdateDishAsync(dishToUpdate, newDish);
			return Ok();
		}
	}
}
