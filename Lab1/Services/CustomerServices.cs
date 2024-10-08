﻿using Lab1.Data.Repos.IRepos;
using Lab1.Exceptions;
using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Models.ViewModels;
using Lab1.Result;
using Lab1.Services.IServices;

namespace Lab1.Services
{
    public class CustomerServices : ICustomerServices
    {
        private readonly Data.Repos.IRepos.ICustomerRepo _customerRepo;

        public CustomerServices(Data.Repos.IRepos.ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<ServiceResult> AddCustomerAsync(CustomerDTO customer)
        {
            // Validate the input before proceeding
            if (customer == null)
            {
				return new ServiceResult
				{
					Success = false,
					Message = "No input"
				};
			}

            if (string.IsNullOrWhiteSpace(customer.LastName) || string.IsNullOrWhiteSpace(customer.Email))
            {
				return new ServiceResult
				{
					Success = false,
					Message = "Fields cannot be blank"
				};
			}

            // If validation passes, proceed to create the Customer object
            var newCustomer = new Customer
            {
                LastName = customer.LastName,
                FirstName = customer.FirstName,
                Email = customer.Email,
            };

            // Save the new customer to the repository
            await _customerRepo.AddCustomerAsync(newCustomer);

			return new ServiceResult
			{
				Success = true,
				Message = "New customer added"
			};
		}

        public async Task<ServiceResult> DeleteCustomerAsync(int id)
        {
            var customerToDelete = await _customerRepo.GetCustomerByIdAsync(id);

            if (customerToDelete == null) 
            {
                return new ServiceResult
                {
                    Success = false,
                    Message = "Customer not found"
                };
            }

            await _customerRepo.DeleteCustomerById(customerToDelete.Id);
			return new ServiceResult
			{
				Success = true,
				Message = "Customer destroyed"
			};
		}

        public Task<IEnumerable<Booking>> GetAllCustomerBookingsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllCustomersAsync()
        {
            var customerList = await _customerRepo.GetAllCustomersAsync();

            var customerViewModelList = customerList.Select(c => new CustomerViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
            }).ToList();

            return customerViewModelList;
        }

        public async Task<CustomerViewModel> GetCustomerByIdAsync(int id) 
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return null;
            }

            var customerViewModel = new CustomerViewModel
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName= customer.LastName,
                Email = customer.Email
            };

            return customerViewModel;
        }

        public async Task<ServiceResult> UpdateCustomerAsync(int id, CustomerDTO newCustomer)
        {
            var customerToUpdate = await _customerRepo.GetCustomerByIdAsync(id);

            if (customerToUpdate == null)
            {
				return new ServiceResult
				{
					Success = false,
					Message = $"Customer with ID:{id} not found"
				};
			}

            var updatedCustomer = new Customer
            {
                LastName = newCustomer.LastName,
                FirstName = newCustomer.FirstName,
                Email = newCustomer.Email
            };

			if (string.IsNullOrWhiteSpace(newCustomer.LastName) || string.IsNullOrWhiteSpace(newCustomer.Email))
			{
				return new ServiceResult { Success = false, Message = "Inputs are blank" };
			}

			await _customerRepo.UpdateCustomerAsync(customerToUpdate, updatedCustomer);
			return new ServiceResult
			{
				Success = true,
				Message = $"Customer with ID:{id} updated"
			};
		}

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            var customer = await _customerRepo.GetCustomerByEmailAsync(email);

            if (customer == null)
            {
                return null;
            }

            return customer;
        }
    }
}
