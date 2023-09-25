using Financial_project.Models;
using Financial_project.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Financial_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        [HttpGet("getAll")]
        public ActionResult<List<CustomerModel>> GetAllCustomers()
        {
            var customers = _customerRepository.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("getCustomer/{identityCard}")]
        public ActionResult<CustomerModel> GetCustomerByIdentityCard(string identityCard)
        {
            var customer = _customerRepository.GetCustomerByIdentityCard(identityCard);
            if (customer != null)
                return Ok(customer);
            return NotFound($"Customer with identity card {identityCard} not found");
        }

        [HttpPost("addCustomer")]
        public async Task<ActionResult<CustomerModel>> Add([FromBody] CustomerModel customer)
        {
            try
            {
                var addedCustomer = await _customerRepository.AddCustomer(customer);
                return Ok(addedCustomer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
