﻿using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Models.ViewModels;
using Lab1.Result;

namespace Lab1.Services.IServices
{
    public interface ICustomerServices
    {
        Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync();
        Task<CustomerViewModel> GetCustomerByIdAsync(int id);
        Task<ServiceResult> AddCustomerAsync(CustomerDTO customer);
        Task<ServiceResult> UpdateCustomerAsync(int id, CustomerDTO newCustomer);
        Task<ServiceResult> DeleteCustomerAsync(int id);
        Task<IEnumerable<Booking>> GetAllCustomerBookingsByIdAsync(int id);
        Task<Customer> GetCustomerByEmailAsync(string email);
    }
}
