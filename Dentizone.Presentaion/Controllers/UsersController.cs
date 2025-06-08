using Dentizone.Application.DTOs.User;
using Dentizone.Application.Interfaces.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dentizone.Domain.Enums;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            try
            {
                var user = await userService.GetByIdAsync(userId!);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

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
                // var updatedUser = await userService.UpdateAsync(id, userDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}/kyc")]
        public async Task<IActionResult> SetKycStatus(string id, [FromBody] KycStatus status)
        {
            try
            {
                await userService.SetKycStatusAsync(id, status);
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