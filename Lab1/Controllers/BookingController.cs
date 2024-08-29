using Lab1.Exceptions;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BookingController : ControllerBase
	{
		private readonly IBookingServices _bookingServices;

		public BookingController(IBookingServices bookingServices)
		{
			_bookingServices = bookingServices;
		}

		// GET: api/booking
		[HttpGet]
		[Route("getallbookings")]
		public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
		{
			var bookings = await _bookingServices.GetAllBookingsAsync();
			return Ok(bookings);
		}

		// GET: api/booking/{id}
		[HttpGet]
		[Route("getbookingbyid/{id}")]
		public async Task<ActionResult<Booking>> GetBookingById(int id)
		{
			var booking = await _bookingServices.GetBookingByIdAsync(id);
			if (booking == null)
			{
				return NotFound();
			}
			return Ok(booking);
		}

		[HttpGet]
		[Route("getcustomerbookingsbyid/{id}")]

		public async Task<ActionResult<IEnumerable<Booking>>> GetCustomerBookingsByCustomerId(int id)
		{
			var bookingList = await _bookingServices.GetCustomerBookingsByCustomerIdAsync(id);
			if (bookingList == null)
			{
				return NotFound();
			}

			return Ok(bookingList);
			
		}

		// POST: api/booking
		[HttpPost]
		[Route("addbooking")]
		public async Task<ActionResult> AddBooking([FromBody] BookingDTO bookingDto, int customerId)
		{
			await _bookingServices.AddBookingAsync(customerId, bookingDto);
			return Ok();
		}

		// PUT: api/booking/{id}
		[HttpPut]
		[Route("updatebookingbyid")]
		public async Task<ActionResult> UpdateBooking(int id, [FromBody] BookingDTO updateBookingDto)
		{
			var booking = await _bookingServices.GetBookingByIdAsync(id);
			if (booking == null)
			{
				return NotFound();
			}

			await _bookingServices.UpdateBooking(id, updateBookingDto);
			return NoContent();
		}

		[HttpPut]
		[Route("addtabletobooking")]
		public async Task<ActionResult> AddTableToBooking(int tableId, int bookingId)
		{
			await _bookingServices.AddTableToBookingByIdAsync(tableId, bookingId);

			return Ok();
		}

		// DELETE: api/booking/{id}
		[HttpDelete]
		[Route("deletebookingbyid")]
		public async Task<ActionResult> DeleteBooking(int id)
		{
			var booking = await _bookingServices.GetBookingByIdAsync(id);
			if (booking == null)
			{
				return NotFound();
			}

			await _bookingServices.DeleteBookingByIdAsync(id);
			return NoContent();
		}
	}
}
