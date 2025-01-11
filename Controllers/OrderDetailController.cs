using API_DEMO.Data;
using API_DEMO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly OrderDetailRepository _orderdetailRepository;
        private readonly ProductRepository _productRepository;

        public OrderDetailController(OrderDetailRepository orderdetailRepository)
        {
            _orderdetailRepository = orderdetailRepository;
        }

        #region GetAllOrderDetails
        [HttpGet("GetAll")]
        public IActionResult GetAllOrderDetails()
        {
            var orderdetails = _orderdetailRepository.SelectAll();
            return Ok(new { Data = orderdetails, Message = "Successfully retrieved orderdetails!" });
        }
        #endregion

        #region GetOrderDetailByID
        [HttpGet("GetOrderDetailByID/{OrderDetailID}")]
        public IActionResult GetOrderDetailByID(int OrderDetailID)
        {
            var orderdetail = _orderdetailRepository.SelectByPK(OrderDetailID);
            if (orderdetail == null)
            {
                return NotFound();
            }
            return Ok(new { Data = orderdetail, Message = "Successfully retrieved orderdetail!" });
        }
        #endregion

        #region DeleteOrderDetail
        [HttpDelete("DeleteOrderDetail/{OrderDetailID}")]
        public IActionResult DeleteOrderDetail(int OrderDetailID)
        {
            var isDeleted = _orderdetailRepository.Delete(OrderDetailID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok(new { Message = "Order Detail deleted successfully!" });
        }
        #endregion

        #region InsertOrderDetail
        [HttpPost("InsertOrderDetail")]
        public IActionResult InsertOrderDetail([FromBody] OrderDetailModel orderdetailModel)
        {
            if (orderdetailModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _orderdetailRepository.Insert(orderdetailModel);
            if (isInserted)
            {
                return Ok(new { Message = "Order Detail inserted successfully!" });
            }
            return StatusCode(500, "An error occurred while inserting the order detail.");
        }
        #endregion

        #region UpdateOrderDetail
        [HttpPut("UpdateOrderDetail/{OrderDetailID}")]
        public IActionResult UpdateOrderDetail(int OrderDetailID, [FromBody] OrderDetailModel orderdetailModel)
        {
            if (orderdetailModel == null || OrderDetailID != orderdetailModel.OrderDetailID)
            {
                return BadRequest(new { Message = "Invalid OrderDetailID or request body." });
            }

            bool isUpdated = _orderdetailRepository.Update(orderdetailModel);
            if (!isUpdated)
            {
                return NotFound(new { Message = "Order Detail not found or update failed." });
            }

            return Ok(new { Message = "Order Detail updated successfully." });
        }
        #endregion

        #region GetOrder
        [HttpGet("order")]
        public IActionResult GetOrder()
        {
            var order = _orderdetailRepository.OrderDropDown();
            if (!order.Any())
            {
                return NotFound("No order found");
            }
            return Ok(new { Data = order, Message = "Successfully retrieved order!" });
        }
        #endregion

        #region GetProduct
        [HttpGet("product")]
        public IActionResult GetProduct()
        {
            var product = _orderdetailRepository.ProductDropDown();
            if (!product.Any())
            {
                return NotFound("No product found");
            }
            return Ok(new { Data = product, Message = "Successfully retrieved product!" });
        }
        #endregion
    }
}
