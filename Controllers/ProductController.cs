using API_DEMO.Data;
using API_DEMO.Models;
using API_DEMO.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;
        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #region GetAllProducts
        [HttpGet("GetAll")]
        public IActionResult GetAllProducts()
        {
            var products = _productRepository.SelectAll();
            return Ok(new { Data = products, Message = "Successfully retrieved products!" });
        }
        #endregion

        #region GetProductByID
        [HttpGet("GetProductByID/{ProductID}")]
        public IActionResult GetProductByID(int ProductID)
        {
            var product = _productRepository.SelectByPK(ProductID);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(new { Data = product, Message = "Successfully retrieved product!" });
        }
        #endregion

        #region DeleteProduct
        [HttpDelete("DeleteProduct/{ProductID}")]
        public IActionResult DeleteProduct(int ProductID)
        {
            var isDeleted = _productRepository.Delete(ProductID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok(new { Message = "City deleted successfully!" });
        }
        #endregion

        #region InsertProduct
        [HttpPost("InsertProduct")]
        public IActionResult InsertProduct([FromBody] ProductModel productModel)
        {
            if (productModel == null)
            {
                return BadRequest();
            }

            bool isInserted = _productRepository.Insert(productModel);
            if (isInserted)
            {
                return Ok(new { Message = "Product inserted successfully!" });
            }
            return StatusCode(500, "An error occurred while inserting the product.");
        }
        #endregion

        #region UpdateProduct
        [HttpPut("UpdateProduct/{ProductID}")]
        public IActionResult UpdateProduct(int ProductID, [FromBody] ProductModel productModel)
        {
            if (productModel == null || ProductID != productModel.ProductID)
            {
                return BadRequest(new { Message = "Invalid ProductID or request body." });
            }

            bool isUpdated = _productRepository.Update(productModel);
            if (!isUpdated)
            {
                return NotFound(new { Message = "Product not found or update failed." });
            }
            Console.WriteLine($"ProductPrice Type: {productModel.ProductPrice.GetType()}");
            return Ok(new { Message = "Product updated successfully." });
        }
        #endregion

        #region GetUser
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            var user = _productRepository.UserDropDown();
            if (!user.Any())
            {
                return NotFound("No user found");
            }
            return Ok(new { Data = user, Message = "Successfully retrieved user!" });
        }
        #endregion
    }
}
