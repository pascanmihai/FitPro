using FitProDB.Models;
using FitProDB.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitProAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [HttpGet("GetCustomer")]
        public async Task<IActionResult>Get(long id)
        {
            var entity = await _customerRepository.GetCustomer(id);
            if(entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult>Add(Customer customer)
        {
            try
            {
                if (customer == null)
                    return BadRequest();
                var added = await _customerRepository.AddCustomer(customer);
                if (added == null)
                    return Problem();

                return Ok(added);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message,statusCode:500);
            }
        }
        [HttpPut("UpdateCustomer")]

        public async Task<IActionResult>UpdateCustomer(long id,Customer customer)
        {
            if(id != customer.Id)
            {
                return BadRequest();
            }
            var entity = await _customerRepository.Update(customer);
            if(entity == null)
            {
                return BadRequest("entity not found");
            }
            return Accepted();
        }
        public async Task<IActionResult>DeleteCustomer(int id)
        {
            try
            {
                await _customerRepository.DeleteCustomer(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "error deleting data");
            }
        }
    }
}
