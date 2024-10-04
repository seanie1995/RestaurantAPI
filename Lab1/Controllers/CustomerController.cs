using Lab1.Models;
using Lab1.Models.DTOs;
using Lab1.Models.ViewModels;
using Lab1.Result;
using Lab1.Services;
using Lab1.Services.IServices;
using Microsoft.AspNetCore.Authorization;
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
        
        [Authorize]
        [HttpGet]
        [Route("getAllCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerViewModel>>> GetAllCustomers()
        {
            var customerList = await _services.GetAllCustomersAsync();

            if (customerList == null)
            {
                return NotFound();
			}

            return Ok(customerList);
        }
        
        [Authorize]
        [HttpGet]
        [Route("getCustomerById/{customerId}")]
        public async Task<ActionResult<CustomerViewModel>> GetCustomerById(int customerId)
        {
            var customer = await _services.GetCustomerByIdAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        [Route("addCustomer")]
        public async Task<ActionResult> AddCustomer(CustomerDTO customer)
        {
            var response = await _services.AddCustomerAsync(customer);
            return Ok(response);
        }
        
        [Authorize]
        [HttpPut]
        [Route("updateCustomerInfo/{customerId}")]
        public async Task<ActionResult> UpdateCustomer(int customerId, CustomerDTO customer)
        {

            var response = await _services.UpdateCustomerAsync(customerId, customer);
            return Ok(response);
        }
       
        [Authorize]
        [HttpDelete]
        [Route("deleteCustomer/{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var response = await _services.DeleteCustomerAsync(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("getCustomerByEmail/{email}")]
        public async Task<ActionResult<int>> GetCustomerIdByEmail(string email)
        {
            var customer = await _services.GetCustomerByEmailAsync(email);

            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            var customerId = customer.Id;

            return Ok(customerId);
        }
    
    
    }
}
