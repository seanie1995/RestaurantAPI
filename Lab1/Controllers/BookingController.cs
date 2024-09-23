using Lab1.Exceptions;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
		[Route("getAllBookings")]
		public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
		{
			var bookings = await _bookingServices.GetAllBookingsAsync();
			return Ok(bookings);
		}

		// GET: api/booking/{id}
		[HttpGet]
		[Route("getBookingById/{bookingId}")]
		public async Task<ActionResult<Booking>> GetBookingById(int bookingId)
		{
			if (bookingId == null)
			{
				return BadRequest("Input cannot be null");
			}

			var booking = await _bookingServices.GetBookingByIdAsync(bookingId);
			if (booking == null)
			{
				return NotFound();
			}
			return Ok(booking);
		}

		[HttpGet]
		[Route("getCustomerBookingsByCustomerId/{customerId}")]

		public async Task<ActionResult<IEnumerable<Booking>>> GetCustomerBookingsByCustomerId(int customerId)
		{
			if (customerId == null)
			{
				return BadRequest("Input cannot be null");
			}

			var bookingList = await _bookingServices.GetCustomerBookingsByCustomerIdAsync(customerId);
			if (bookingList == null)
			{
				return NotFound();
			}

			return Ok(bookingList);
			
		}

		// POST: api/booking
		[HttpPost]
		[Route("addBooking/{customerId}")]
		public async Task<ActionResult> AddBooking([FromBody] BookingDTO bookingDto, int customerId)
		{
			if (customerId == null)
			{
				return BadRequest("Input cannot be null");
			}

			await _bookingServices.AddBookingAsync(customerId, bookingDto);
			return Ok();
		}

		// PUT: api/booking/{id}
		[HttpPut]
		[Route("updateBookingById/{bookingId}")]
		public async Task<ActionResult> UpdateBooking(int bookingId, [FromBody] BookingDTO updatedBookingDto)
		{
			if (bookingId == null)
			{
				return BadRequest("Input cannot be null");
			}

			var booking = await _bookingServices.GetBookingByIdAsync(bookingId);
			if (booking == null)
			{
				return NotFound();
			}

			await _bookingServices.UpdateBookingAsync(bookingId, updatedBookingDto);
			return NoContent();
		}

		//[HttpPatch]
		//[Route("updateBookingTable/{bookingId}")]
		//public async Task<ActionResult> UpdateBookingTable(int tableId, int bookingId)
		//{
		//	if (tableId == null || bookingId == null)
		//	{
		//		return BadRequest("Input cannot be null");
		//	}

		//	await _bookingServices.UpdateBookingTableAsync(tableId, bookingId);

		//	return Ok();
		//}

		//[HttpPatch]
		//[Route("updateBookingPartySize/{bookingId}")]
		//public async Task<ActionResult> UpdateBookingPartySize(int bookingId, int partySize)
		//{
		//	if (bookingId == null || partySize == 0)
		//	{
		//		return BadRequest("Input cannot be null");
		//	}

		//	await _bookingServices.UpdateBookingPartySizeAsync(bookingId, partySize);

		//	return Ok();
		//}

		//[HttpPatch]
		//[Route("updateBookingTime/{bookingId}")]
		//public async Task<ActionResult> UpdateBookingTime(int bookingId, [FromBody] BookingTimeDTO newBookingTime)
		//{
		//	if (bookingId == null || newBookingTime.BookingStart == null || newBookingTime.BookingEnd == null)
		//	{
		//		return BadRequest("Input cannot be null");
		//	}

		//	await _bookingServices.UpdateBookingTimeAsync(bookingId, newBookingTime);

		//	return Ok();
		//}

		// DELETE: api/booking/{id}
		[HttpDelete]
		[Route("deleteBookingById/{bookingId}")]
		public async Task<ActionResult> DeleteBooking(int bookingId)
		{
			if (bookingId == null || bookingId == 0)
			{
				return BadRequest("Input cannot be null");
			}

			await _bookingServices.DeleteBookingByIdAsync(bookingId);
			return NoContent();
		}
	}
}
