using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Models;
using WebApi.Service;
using System.Net;

namespace WebApi.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet("{id:long}")]   
        public async Task<ActionResult<Customer>> GetCustomerAsync([FromRoute] long id)
        {
            var customer = await _customerService.GetCustomerAsync(id);
            
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost("")]   
        public async Task<IActionResult> CreateCustomerAsync([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required.");
            }

            var createdId = await _customerService.AddCustomerAsync(customer.Firstname, customer.Lastname);

            var response = new
            {
                Id = createdId,
                Status = "Customer created successfully"
            };

            return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.Created };
        }
    }
}