using API_DEMO.Data;
using API_DEMO.Models;
using Microsoft.AspNetCore.Mvc;

namespace API_DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;
        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        #region GetAllOrders
        [HttpGet("GetAll")]
        public IActionResult GetAllOrders()
        {
            var orders = _orderRepository.SelectAll();
            return Ok(new { Data = orders, Message = "Successfully retrieved orders!" });
        }
        #endregion

        #region GetOrderByID
        [HttpGet("GetOrderByID/{OrderID}")]
        public IActionResult GetOrderByID(int OrderID)
        {
            var order = _orderRepository.SelectByPK(OrderID);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(new { Data = order, Message = "Successfully retrieved order!" });
        }
        #endregion

        #region DeleteOrder
        [HttpDelete("DeleteOrder/{OrderID}")]
        public IActionResult DeleteOrder(int OrderID)
        {
            var isDeleted = _orderRepository.Delete(OrderID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok(new { Message = "Order deleted successfully!" });
        }
        #endregion

        #region InsertOrder
        [HttpPost("InsertOrder")]
        public IActionResult InsertOrder([FromBody] OrderModel orderModel)
        {
            if (orderModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _orderRepository.Insert(orderModel);
            if (isInserted)
            {
                return Ok(new { Message = "Order inserted successfully!" });
            }
            return StatusCode(500, "An error occurred while inserting the order.");
        }
        #endregion

        #region UpdateOrder
        [HttpPut("UpdateOrder/{OrderID}")]
        public IActionResult UpdateOrder(int OrderID, [FromBody] OrderModel orderModel)
        {
            if (orderModel == null || OrderID != orderModel.OrderID)
            {
                return BadRequest(new { Message = "Invalid OrderID or request body." });
            }

            bool isUpdated = _orderRepository.Update(orderModel);
            if (!isUpdated)
            {
                return NotFound(new { Message = "Order not found or update failed." });
            }

            return Ok(new { Message = "Order updated successfully." });
        }
        #endregion

        #region GetCustomer
        [HttpGet("customer")]
        public IActionResult GetCustomer()
        {
            var customer = _orderRepository.CustomerDropDown();
            if (!customer.Any())
            {
                return NotFound("No customer found");
            }
            return Ok(new { Data = customer, Message = "Successfully retrieved customer!" });
        }
        #endregion
    }
}
