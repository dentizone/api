using Dentizone.Application.DTOs.UserActivity;
using Dentizone.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController(IUserActivityService userActivityService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetActivities([FromQuery] ActivityFilterDto filters)
        {
            var activities = await userActivityService.GetAll(filters);

            return Ok(activities);
        }
    }
}