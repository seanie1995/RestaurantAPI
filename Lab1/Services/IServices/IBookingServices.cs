﻿using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Models.ViewModels;

namespace Lab1.Services.IServices
{
    public interface IBookingServices
    {
        Task<IEnumerable<BookingViewModel>> GetAllBookingsAsync();
        Task<BookingViewModel> GetBookingByIdAsync(int id);
        Task AddBookingAsync(int customerId,  BookingDTO booking);
        Task UpdateBookingAsync(int id, BookingDTO updateBooking);
        Task DeleteBookingByIdAsync(int id);
        Task<IEnumerable<BookingViewModel>>GetCustomerBookingsByCustomerIdAsync(int customerId);
		Task AddTableToBookingByIdAsync(int tableId, int bookingId);
        Task<bool> CheckIfTableIsAvailableAsync(int tableId, DateTime bookingStart, DateTime bookingEnd);
        Task<bool> CheckIfTableHasEnoughSeatsAsync(int tableId, int partySize);
	}
}
