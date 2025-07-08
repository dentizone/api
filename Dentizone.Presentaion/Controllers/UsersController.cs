using Dentizone.Application.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Dentizone.Application.Interfaces;
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


            var user = await userService.GetByIdAsync(userId!);
            return Ok(user);
        }

        [Authorize("IsAdmin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await userService.GetByIdAsync(id);
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Policy = "IsAdmin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserFilterDto userFilter)
        {
            var users = await userService.GetAllAsync(userFilter);
            return Ok(users);
        }

        [HttpDelete("{id}")]
        [Authorize("IsAdmin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var deletedUser = await userService.DeleteAsync(id);
            return Ok(deletedUser);
        }


        [HttpPatch("{id}/kyc")]
        public async Task<IActionResult> SetKycStatus(string id, [FromBody] KycStatus status)
        {
            await userService.SetKycStatusAsync(id, status);
            return NoContent();
        }

        [Authorize("IsAdmin")]
        [HttpPatch("{id}/state")]
        public async Task<IActionResult> SetUserState(string id, [FromBody] UserStateDto userStateDto)
        {
            await userService.SetUserStateAsync(id, userStateDto);
            return NoContent();
        }

        [HttpGet("stats")]
        [Authorize("IsAdmin")]
        public async Task<IActionResult> GetUserStats()
        {
            var userStats = await userService.GetUserStatsAsync();
            return Ok(userStats);
        }
    }
}