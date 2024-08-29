using Lab1.Models;
using Lab1.Services;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Lab1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TableController : ControllerBase
	{
		private readonly ITableServices _tableServices;

		public TableController(ITableServices tableServices)
		{
			_tableServices = tableServices;
		}

		[HttpGet]
		[Route("getalltables")]
		public async Task<ActionResult<IEnumerable<Table>>> GetAllTables()
		{
			var tableList = await _tableServices.GetAllTablesAsync();
			return Ok(tableList);
		}

		[HttpGet]
		[Route("gettablebyid/{id}")]
		public async Task<ActionResult<Table>> GetTableById(int id)
		{
			var table = await _tableServices.GetTableByIdAsync(id);

			return Ok(table);
		}


		[HttpGet]
		[Route("gettablebookingsbyid")]

		public async Task<ActionResult<Booking>> GetAllTableBookingsById(int id)
		{
			var bookingsList = await _tableServices.GetBookingsConnectedToTableByIdAsync(id);

			return Ok(bookingsList);
		}
		[HttpPost]
		[Route("addtable")]

		public async Task<ActionResult> AddNewTable(int capacity)
		{
			await _tableServices.AddNewTableAsync(capacity);
			return Ok();
		}

		[HttpDelete]
		[Route("deletetable")]

		public async Task<ActionResult> DeleteTableById(int id)
		{
			await _tableServices.DeleteTableByIdAsync(id);
			return Ok();
		}

		[HttpPut]
		[Route("updatetable")]
		public async Task<ActionResult> UpdateTableById(int capacity, int id)
		{
			await _tableServices.UpdateTableAsync(capacity, id);
			return Ok();
		}


    }
}
