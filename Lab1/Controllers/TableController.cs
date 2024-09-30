﻿using Lab1.Models;
using Lab1.Models.ViewModels;
using Lab1.Services;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Lab1.Controllers
{
	[Authorize]
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
		[Route("getAllTables")]
		public async Task<ActionResult<IEnumerable<Table>>> GetAllTables()
		{
			var tableList = await _tableServices.GetAllTablesAsync();
			return Ok(tableList);
		}

		[HttpGet]
		[Route("getTableById/{tableId}")]
		public async Task<ActionResult<Table>> GetTableById(int tableId)
		{
			var table = await _tableServices.GetTableByIdAsync(tableId);

			return Ok(table);
		}


		[HttpGet]
		[Route("getTableBookingsById/{tableId}")]

		public async Task<ActionResult<BookingViewModel>> GetAllTableBookingsByTableId(int tableId)
		{
			var bookingsList = await _tableServices.GetBookingsConnectedToTableByIdAsync(tableId);

			return Ok(bookingsList);
		}
		[HttpPost]
		[Route("addTable/{capacity}")]

		public async Task<ActionResult> AddNewTable(int capacity)
		{
			await _tableServices.AddNewTableAsync(capacity);
			return Ok();
		}

		[HttpDelete]
		[Route("deleteTableById/{id}")]

		public async Task<ActionResult> DeleteTableById(int id)
		{
			await _tableServices.DeleteTableByIdAsync(id);
			return Ok();
		}

		[HttpPut]
		[Route("updateTable/{id}/{newCapacity}")]
		public async Task<ActionResult> UpdateTableById(int newCapacity, int id)
		{
			await _tableServices.UpdateTableAsync(newCapacity, id);
			return Ok();
		}


    }
}
