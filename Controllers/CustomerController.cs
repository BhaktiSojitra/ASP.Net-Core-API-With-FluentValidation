using API_DEMO.Data;
using API_DEMO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;
        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        #region GetAllCustomers
        [HttpGet("GetAll")]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerRepository.SelectAll();
            return Ok(new { Data = customers, Message = "Successfully retrieved customers!" });
        }
        #endregion

        #region GetCustomerByID
        [HttpGet("GetCustomerByID/{CustomerID}")]
        public IActionResult GetCustomerByID(int CustomerID)
        {
            var customer = _customerRepository.SelectByPK(CustomerID);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(new { Data = customer, Message = "Successfully retrieved customer!" });
        }
        #endregion

        #region DeleteCustomer
        [HttpDelete("DeleteCustomer/{CustomerID}")]
        public IActionResult DeleteCustomer(int CustomerID)
        {
            var isDeleted = _customerRepository.Delete(CustomerID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok(new { Message = "Customer deleted successfully!" });
        }
        #endregion

        #region InsertCustomer
        [HttpPost("InsertCustomer")]
        public IActionResult InsertCustomer([FromBody] CustomerModel customerModel)
        {
            if (customerModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _customerRepository.Insert(customerModel);
            if (isInserted)
            {
                return Ok(new { Message = "Customer inserted successfully!" });
            }
            return StatusCode(500, "An error occurred while inserting the customer.");
        }
        #endregion

        #region UpdateCustomer
        [HttpPut("UpdateCustomer/{CustomerID}")]
        public IActionResult UpdateUser(int CustomerID, [FromBody] CustomerModel customerModel)
        {
            if (customerModel == null || CustomerID != customerModel.CustomerID)
            {
                return BadRequest(new { Message = "Invalid CustomerID or request body." });
            }

            bool isUpdated = _customerRepository.Update(customerModel);
            if (!isUpdated)
            {
                return NotFound(new { Message = "Customer not found or update failed." });
            }

            return Ok(new { Message = "Customer updated successfully." });
        }
        #endregion
    }
}
