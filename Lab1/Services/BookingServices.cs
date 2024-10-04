using Lab1.Data.Repos;
using Lab1.Data.Repos.IRepos;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Models.ViewModels;
using Lab1.Result;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Server.IIS;
using System.Runtime.InteropServices.Marshalling;

namespace Lab1.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepo _bookingRepo;   
        private readonly ITableRepo _tableRepo;
        private readonly ICustomerRepo _customerRepo;
        public BookingServices(ITableRepo tableRepo ,IBookingRepo bookingRepo, ICustomerRepo customerRepo )
        {
            _bookingRepo = bookingRepo;
            _tableRepo = tableRepo;
            _customerRepo = customerRepo;
        }

		// Method below takes a customerId and a bookingDTO. Customer is found and it's Id is automatically added to new booking
		public async Task<ServiceResult> AddBookingAsync(int customerId, BookingDTO booking) 
        {
            var newCustomer = await _customerRepo.GetCustomerByIdAsync(customerId);
		    
            if (newCustomer == null)
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Customer input is null"
                };
            }

			var hasSeats = await CheckIfTableHasEnoughSeatsAsync(booking.TableId, booking.PartySize);
			var hasAvailableTime = await CheckIfTableIsAvailableAsync(booking.TableId, booking.BookingStart, booking.BookingEnd);

			if (!hasSeats)
			{
                return new ServiceResult
                {
                    Success = false,
                    Message = "Table does not have enough seats"
                };
               
			}
			if (!hasAvailableTime)
			{
                return new ServiceResult
                {
                    Success = false,
                    Message = "Table is already booked for this time slot"
                };
                
			}

			var newBooking = new Booking
            {
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd,
                PartySize = booking.PartySize,
                FK_CustomerId = newCustomer.Id,
                FK_TableId = booking.TableId
                
            };

            await _bookingRepo.AddBookingAsync(newBooking);

            return new ServiceResult
            {
                Success = true,
                Message = "Booking Success"
            };
        }
       
        public async Task<ServiceResult> DeleteBookingByIdAsync(int id)
        {          
            await _bookingRepo.DeleteBookingByIdAsync(id);
            return new ServiceResult
            {
                Success = true,
                Message = "Delete Success"
            };
        }
    
        public async Task<IEnumerable<BookingViewModel>> GetAllBookingsAsync()
        {
            var bookingList = await _bookingRepo.GetAllBookingsAsync();

            if (bookingList == null)
            {
                return null;
            }

            var bookingListViewModels = bookingList.Select(b => new BookingViewModel
            {
                Id = b.Id,
                PartySize = b.PartySize,
                BookingStart = b.BookingStart,
                BookingEnd = b.BookingEnd,
                CustomerId = b.FK_CustomerId,
				TableId = b.FK_TableId
			}).ToList();

            return bookingListViewModels;
        }
        
        public async Task<BookingViewModel> GetBookingByIdAsync(int id)
        {
            var booking = await _bookingRepo.GetBookingByIdAsync(id);

            if (booking == null)
            {
                return null;
            }

            var bookingViewModel = new BookingViewModel
            {
                Id = booking.Id,
                PartySize = booking.PartySize,
                BookingStart = booking.BookingStart,
                BookingEnd = booking.BookingEnd,
                CustomerId = booking.FK_CustomerId,
                TableId = booking.FK_TableId
            };

            return bookingViewModel;
        }

        public async Task<ServiceResult> UpdateBookingAsync(int existingBookingId, BookingDTO updatedBooking) 
        {
            var bookingToUpdate = await _bookingRepo.GetBookingByIdAsync(existingBookingId);

            var tableToCheck = await _tableRepo.GetTableByIdAsync(bookingToUpdate.FK_TableId);
           
			var hasSeats = await CheckIfTableHasEnoughSeatsAsync(tableToCheck.Id, updatedBooking.PartySize);
			
            if (!hasSeats)
			{
                return new ServiceResult
                {
                    Success = false,
                    Message = "Table does not have enough seats"
                };
			}

            bool timesChanged = updatedBooking.BookingStart != bookingToUpdate.BookingStart || updatedBooking.BookingEnd != bookingToUpdate.BookingEnd || updatedBooking.CustomerId != bookingToUpdate.FK_CustomerId;
                
			if (timesChanged || updatedBooking.TableId != bookingToUpdate.FK_TableId)
			{
				var hasAvailableTime = await CheckIfTableIsAvailableAsync(updatedBooking.TableId, updatedBooking.BookingStart, updatedBooking.BookingEnd);
				if (!hasAvailableTime)
				{
                    return new ServiceResult
                    {
                        Success = false,
                        Message = "Table is already booked for this time slot"
                    };
                }
			}
		          
			var newBooking2 = new Booking
			{
				PartySize = updatedBooking.PartySize,
				BookingStart = updatedBooking.BookingStart,
				BookingEnd = updatedBooking.BookingEnd,
                FK_TableId = updatedBooking.TableId,
			};

			await _bookingRepo.UpdateBookingAsync(bookingToUpdate, newBooking2);
            return new ServiceResult
            {
                Success = true,
                Message = "Booking Updated"
            };

        }

        public async Task<IEnumerable<BookingViewModel>> GetCustomerBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await _bookingRepo.GetCustomerBookingsByCustomerIdAsync(customerId);

            if (bookings == null)
            {
                return null;
            }

            var bookingsViewModels = bookings.Select(static b => new BookingViewModel
            {
                Id = b.Id,
                PartySize = b.PartySize,
                BookingStart = b.BookingStart,
                BookingEnd = b.BookingEnd,
                CustomerId = b.FK_CustomerId,
                TableId = b.FK_TableId
            }).ToList();

            return bookingsViewModels;
        }

        public async Task AddBookingByCustomer(string email, BookingDTO booking)
        {
            var customer = await _customerRepo.GetCustomerByEmailAsync(email);

            if (customer == null) 
            {
                throw new Exception("Customer does not exist");
            }

            await AddBookingAsync(customer.Id, booking);
        }
        public async Task<bool> CheckIfTableIsAvailableAsync(int tableId, DateTime bookingStart, DateTime bookingEnd)
		{
			var bookingsList = await _tableRepo.GetBookingsConnectedToTableByIdAsync(tableId);
					
			foreach (var booking in bookingsList)
			{		
				// Check for overlap
				if ((booking.BookingStart == bookingStart && booking.BookingEnd == bookingEnd) ||
	                (booking.BookingStart <= bookingEnd && booking.BookingEnd >= bookingStart))
				{
					return false;
				}

			}
			return true;
		}

		public async Task<bool> CheckIfTableHasEnoughSeatsAsync(int tableId, int partySize)
        {
            var table = await _tableRepo.GetTableByIdAsync(tableId);
                     
            return partySize <= table.Capacity;
        }
	}
}
