using Dentizone.Application.DTOs.University;

namespace Dentizone.Application.Interfaces
{
    internal interface IUniversityService
    {
        Task<IReadOnlyList<SupportedUniversitiesDto>> GetSupportedUniversitiesAsync();
        Task<UniversityDto> DeleteUniversity(string id);
        Task<CreateUniversityDto> CreateUniversityAsync(CreateUniversityDto universityDto);
        Task<UpdateUniversityDto> UpdateUniversityAsync(string id, UpdateUniversityDto updateUniversityDTO);
    }
}