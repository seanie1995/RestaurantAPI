﻿using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Models.ViewModels;
using Lab1.Services;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase

    {
        private readonly ICustomerServices _services;

        public CustomerController(ICustomerServices services)
        {
            _services = services;
        }

        [HttpGet]
        [Route("getallcustomers")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetAllCustomers()
        {
            var customerList = await _services.GetAllCustomersAsync();
            return Ok(customerList);
        }

        [HttpGet]
        [Route("customer/{customerId}")]
        public async Task<ActionResult<CustomerViewModel>> GetCustomerById(int customerId)
        {
            var customer = await _services.GetCustomerByIdAsync(customerId);

            return customer;
        }

        [HttpPost]
        [Route("addcustomer")]
        public async Task<ActionResult> AddCustomer(CustomerDTO customer)
        {
            await _services.AddCustomerAsync(customer);
            return Ok($"Customer with email: {customer.Email} has been added");
        }

        [HttpPatch]
        [Route("updatecustomerinfo")]
        public async Task<ActionResult> UpdateCustomer(int customerId, CustomerDTO customer)
        {
           
            await _services.UpdateCustomerAsync(customerId, customer);
            return Ok("Customer information updated");
        }

        [HttpDelete]
        [Route("deleteCustomer/{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _services.DeleteCustomerAsync(id);
            return Ok("Customer deleted");
        }
    }
}
