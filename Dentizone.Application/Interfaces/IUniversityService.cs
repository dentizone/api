using Dentizone.Application.DTOs.University;

namespace Dentizone.Application.Interfaces
{
    public interface IUniversityService
    {
        Task<UniversityDto> GetUniversityByIdAsync(string id);
        Task<IReadOnlyList<UniversityDto>> GetAllUniversitiesAsync();
        Task<IReadOnlyList<SupportedUniversitiesDto>> GetSupportedUniversitiesAsync();
        Task<UniversityDto> DeleteUniversity(string id);
        Task<UniversityView> CreateUniversityAsync(CreateUniversityDto universityDto);
        Task<UpdateUniversityDto> UpdateUniversityAsync(string id, UpdateUniversityDto updateUniversityDto);
    }
}