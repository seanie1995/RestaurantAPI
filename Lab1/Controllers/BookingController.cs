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
		[Route("getbookingbyid/{bookingId}")]
		public async Task<ActionResult<Booking>> GetBookingById(int bookingId)
		{
			var booking = await _bookingServices.GetBookingByIdAsync(bookingId);
			if (booking == null)
			{
				return NotFound();
			}
			return Ok(booking);
		}

		[HttpGet]
		[Route("getcustomerbookingsbyid/{customerId}")]

		public async Task<ActionResult<IEnumerable<Booking>>> GetCustomerBookingsByCustomerId(int customerId)
		{
			var bookingList = await _bookingServices.GetCustomerBookingsByCustomerIdAsync(customerId);
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
		[HttpPatch]
		[Route("updatebookingbyid")]
		public async Task<ActionResult> UpdateBooking(int existingBookingId, [FromBody] BookingDTO updatedBookingDto)
		{
			var booking = await _bookingServices.GetBookingByIdAsync(existingBookingId);
			if (booking == null)
			{
				return NotFound();
			}

			await _bookingServices.UpdateBookingAsync(existingBookingId, updatedBookingDto);
			return NoContent();
		}

		[HttpPatch]
		[Route("addtabletobooking")]
		public async Task<ActionResult> AddTableToBooking(int tableId, int bookingId)
		{
			await _bookingServices.AddTableToBookingByIdAsync(tableId, bookingId);

			return Ok();
		}

		// DELETE: api/booking/{id}
		[HttpDelete]
		[Route("deletebookingbyid")]
		public async Task<ActionResult> DeleteBooking(int bookingId)
		{
			var booking = await _bookingServices.GetBookingByIdAsync(bookingId);
			if (booking == null)
			{
				return NotFound();
			}

			await _bookingServices.DeleteBookingByIdAsync(bookingId);
			return NoContent();
		}
	}
}
