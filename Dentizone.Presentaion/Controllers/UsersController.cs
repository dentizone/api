using Dentizone.Application.DTOs.User;
using Dentizone.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(UserService userService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await userService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers(int page, string? search = null)
        {
            var users = await userService.GetAllAsync(page, search);
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var createdUser = await userService.CreateAsync(userDto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var deletedUser = await userService.DeleteAsync(id);
                return Ok(deletedUser);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto userDto)
        {
            try
            {
                var updatedUser = await userService.UpdateAsync(id, userDto);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}/kyc")]
        public async Task<IActionResult> SetKycStatus(string id, [FromBody] KycStatusDTO kycStatusDto)
        {
            try
            {
                await userService.SetKycStatusAsync(id, kycStatusDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}/state")]
        public async Task<IActionResult> SetUserState(string id, [FromBody] UserStateDTO userStateDto)
        {
            try
            {
                await userService.SetUserStateAsync(id, userStateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}