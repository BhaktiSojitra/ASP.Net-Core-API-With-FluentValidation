using API_DEMO.Data;
using API_DEMO.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_DEMO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region GetAllUsers
        [HttpGet("GetAll")]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.SelectAll();
            return Ok(new { Data = users, Message = "Successfully retrieved users!" });
        }
        #endregion

        #region GetUserByID
        [HttpGet("GetUserByID/{UserID}")]
        public IActionResult GetUserByID(int UserID)
        {
            var user = _userRepository.SelectByPK(UserID);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new { Data = user, Message = "Successfully retrieved user!" });
        }
        #endregion

        #region DeleteUser
        [HttpDelete("DeleteUser/{UserID}")]
        public IActionResult DeleteUser(int UserID)
        {
            var isDeleted = _userRepository.Delete(UserID);
            if (!isDeleted)
            {
                return NotFound();
            }
            return Ok(new { Message = "User deleted successfully!" });
        }
        #endregion

        #region InsertUSer
        [HttpPost("InsertUSer")]
        public IActionResult InsertUser([FromBody] UserModel userModel)
        {
            if (userModel == null)
            {
                return BadRequest();
            }
            bool isInserted = _userRepository.Insert(userModel);
            if (isInserted)
            {
                return Ok(new { Message = "User inserted successfully!" });
            }
            return StatusCode(500, "An error occurred while inserting the user.");
        }
        #endregion

        #region UpdateUser
        [HttpPut("UpdateUser/{UserID}")]
        public IActionResult UpdateUser(int UserID, [FromBody] UserModel userModel)
        {
            if (userModel == null || UserID != userModel.UserID)
            {
                return BadRequest(new { Message = "Invalid UserID or request body." });
            }

            bool isUpdated = _userRepository.Update(userModel);
            if (!isUpdated)
            {
                return NotFound(new { Message = "User not found or update failed." });
            }

            return Ok(new { Message = "User updated successfully." });
        }
        #endregion
    }
}
