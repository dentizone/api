using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dentizone.Application.Services;
using Dentizone.Application.Interfaces;
using Dentizone.Application.DTOs.University;
namespace Dentizone.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private readonly IUniversityService _universityService;
        public UniversitiesController(IUniversityService universityService)
        {
            _universityService = universityService;
        }
        [HttpGet("supported")]
        public async Task<IActionResult> GetAllUniversities()
        {
            var universities = await _universityService.GetSupportedUniversitiesAsync();
            return Ok(universities);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUniversityById(string id)
        {
            try
            {
                var university = await _universityService.GetUniversityByIdAsync(id);
                return Ok(university);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUniversity([FromBody] CreateUniversityDto universityDto)
        {
            if (universityDto == null)
            {
                return BadRequest("University data is null.");
            }
            var createdUniversity = await _universityService.CreateUniversityAsync(universityDto);
            return CreatedAtAction(nameof(GetUniversityById), new { id = createdUniversity.Id }, createdUniversity);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUniversity(string id, [FromBody] UpdateUniversityDto updateUniversityDto)
        {
            if (updateUniversityDto == null)
            {
                return BadRequest("University data is null.");
            }
            try
            {
                var updatedUniversity = await _universityService.UpdateUniversityAsync(id, updateUniversityDto);
                return Ok(updatedUniversity);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUniversity(string id)
        {
            try
            {
                var deletedUniversity = await _universityService.DeleteUniversity(id);
                return Ok(deletedUniversity);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
