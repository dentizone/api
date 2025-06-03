using Dentizone.Application.DTOs.University;
using Dentizone.Domain.Entity;

namespace Dentizone.Application.Interfaces
{
    internal interface IUniversityService
    {
        Task<IReadOnlyList<SupportedUniversitiesDto>> GetSupportedUniversitiesAsync();
        Task<University> DeleteUniversity(string id);
        Task<CreateUniversityDto> CreateUniversityAsync(CreateUniversityDto universityDto);
        Task<UpdateUniversityDTO> UpdateUniversityAsync(string id, UpdateUniversityDTO updateUniversityDTO);
    }
}