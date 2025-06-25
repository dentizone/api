using Dentizone.Application.DTOs;
using Dentizone.Application.DTOs.University;

namespace Dentizone.Application.Interfaces
{
    public interface IUniversityService
    {
        Task<PagedResultDto<UniversityDto>> GetAllUniversitiesAsync(int page = 1);
        Task<UniversityDto> GetUniversityByIdAsync(string id);
        Task<IReadOnlyList<SupportedUniversitiesDto>> GetSupportedUniversitiesAsync();
        Task<UniversityDto> DeleteUniversity(string id);
        Task<UniversityView> CreateUniversityAsync(CreateUniversityDto universityDto);
        Task<UpdateUniversityDto> UpdateUniversityAsync(string id, UpdateUniversityDto updateUniversityDto);
    }
}