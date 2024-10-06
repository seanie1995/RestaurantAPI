using Lab1.Exceptions;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
        [Authorize]
        [HttpGet]
		[Route("getAllBookings")]
		public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
		{
			var bookings = await _bookingServices.GetAllBookingsAsync();
			return Ok(bookings);
		}

        // GET: api/booking/{id}
        [Authorize]
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
        [Authorize]
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

			var response = await _bookingServices.AddBookingAsync(customerId, bookingDto);
			return Ok(response);
		}

        // PUT: api/booking/{id}
        [Authorize]
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

			var response = await _bookingServices.UpdateBookingAsync(bookingId, updatedBookingDto);

			if (response.Success == false)	
			{
				return BadRequest($"{response.Message}");
			}

			return Ok(response);
		}


        // DELETE: api/booking/{id}
        [Authorize]
        [HttpDelete]
		[Route("deleteBookingById/{bookingId}")]
		public async Task<ActionResult> DeleteBooking(int bookingId)
		{
			if (bookingId == null || bookingId == 0)
			{
				return BadRequest("Input cannot be null");
			}

			var response = await _bookingServices.DeleteBookingByIdAsync(bookingId);
			return Ok(response);
		}
	}
}
