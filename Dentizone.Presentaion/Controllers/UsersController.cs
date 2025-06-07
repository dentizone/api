using Dentizone.Application.DTOs.User;
using Dentizone.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService) 
        {
            _userService = userService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int page, string? search = null)
        {
            var users = await _userService.GetAllAsync(page, search);
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDTO)
        {
            var createdUser = await _userService.CreateAsync(userDTO);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var deletedUser = await _userService.DeleteAsync(id);
            return Ok(deletedUser);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTO userDTO)
        {
            var updatedUser = await _userService.UpdateAsync(id, userDTO);
            return Ok(updatedUser);
        }
        [HttpPost("{id}/kyc")]
        public async Task<IActionResult> SetKycStatus(string id, [FromBody] KycStatusDTO kycStatusDTO)
        {
            await _userService.SetKycStatusAsync(id, kycStatusDTO);
            return NoContent();
        }
        [HttpPost("{id}/state")]
        public async Task<IActionResult> SetUserState(string id, [FromBody] UserStateDTO userStateDTO)
        {
            await _userService.SetUserStateAsync(id, userStateDTO);
            return NoContent();

        }
    }
}
