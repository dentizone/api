using Dentizone.Application.DTOs.University;
using Dentizone.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController(IUniversityService universityService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1)
        {
            var universities = await universityService.GetAllUniversitiesAsync(page);
            return Ok(universities);
        }

        [HttpGet("supported")]
        public async Task<IActionResult> GetAllUniversities()
        {
            var universities = await universityService.GetSupportedUniversitiesAsync();
            return Ok(universities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUniversityById(string id)
        {
            var university = await universityService.GetUniversityByIdAsync(id);
            return Ok(university);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUniversity([FromBody] CreateUniversityDto universityDto)
        {
            var createdUniversity = await universityService.CreateUniversityAsync(universityDto);
            return CreatedAtAction(nameof(GetUniversityById), new { id = createdUniversity.Id }, createdUniversity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUniversity(string id, [FromBody] UpdateUniversityDto updateUniversityDto)
        {
            var updatedUniversity = await universityService.UpdateUniversityAsync(id, updateUniversityDto);
            return Ok(updatedUniversity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUniversity(string id)
        {
            var deletedUniversity = await universityService.DeleteUniversity(id);
            return Ok(deletedUniversity);
        }
    }
}