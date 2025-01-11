using API_DEMO.Data;
using API_DEMO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly BillsRepository _billsRepository;
        public BillsController(BillsRepository biilsRepository)
        {
            _billsRepository = biilsRepository;
        }

        #region GetAllBills
        [HttpGet("GetAll")]
        public IActionResult GetAllBills()
        {
            var bills = _billsRepository.SelectAll();
            return Ok(new { Data = bills, Message = "Successfully retrieved bills!" });
        }
        #endregion

        #region GetBillByID
        [HttpGet("GetBillByID/{BillID}")]
        public IActionResult GetBillByID(int BillID)
        {
            var bill = _billsRepository.SelectByPK(BillID);
            if (bill == null)
            {
                return NotFound();
            }
            return Ok(new { Data = bill, Message = "Successfully retrieved bill!" });
        }
        #endregion

        #region DeleteBill
        [HttpDelete("DeleteBill/{BillID}")]
        public IActionResult DeleteUser(int BillID)
        {
            var isDeleted = _billsRepository.Delete(BillID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok(new { Message = "Bill deleted successfully!" });
        }
        #endregion

        #region InsertBill
        [HttpPost("InsertBill")]
        public IActionResult InsertBill([FromBody] BillsModel billsModel)
        {
            if (billsModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _billsRepository.Insert(billsModel);
            if (isInserted)
            {
                return Ok(new { Message = "Bill inserted successfully!" });
            }
            return StatusCode(500, "An error occurred while inserting the bill.");
        }
        #endregion

        #region UpdateBill
        [HttpPut("UpdateBill/{BillID}")]
        public IActionResult UpdateBill(int BillID, [FromBody] BillsModel billsModel)
        {
            if (billsModel == null || BillID != billsModel.BillID)
            {
                return BadRequest(new { Message = "Invalid BillID or request body." });
            }

            bool isUpdated = _billsRepository.Update(billsModel);
            if (!isUpdated)
            {
                return NotFound(new { Message = "Bill not found or update failed." });
            }

            return Ok(new { Message = "Bill updated successfully." });
        }
        #endregion
    }
}