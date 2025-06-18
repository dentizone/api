using AutoMapper;
using Dentizone.Application.DTOs.University;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Repositories;

namespace Dentizone.Application.Services
{
    public class UniversityService(IMapper mapper, IUniversityRepository repo) : IUniversityService
    {
        public async Task<UniversityView> CreateUniversityAsync(CreateUniversityDto universityDto)
        {
            var newUniversity = await repo.CreateAsync(mapper.Map<University>(universityDto));
            return mapper.Map<UniversityView>(newUniversity);
        }

        public async Task<UniversityDto> DeleteUniversity(string id)
        {
            var deleted = await repo.DeleteAsync(id);

            return mapper.Map<UniversityDto>(deleted) ??
                   throw new NotFoundException("No University found with this id. Please check the id and try again.");
        }

        public async Task<IReadOnlyList<UniversityDto>> GetAllUniversitiesAsync()
        {
            var universities = await repo.GetAll();
            return mapper.Map<IReadOnlyList<UniversityDto>>(universities);
        }

        public async Task<IReadOnlyList<SupportedUniversitiesDto>> GetSupportedUniversitiesAsync()
        {
            var universities = await repo.GetAll();
            var supported = universities.Where(u => u.IsSupported).ToList();
            return mapper.Map<IReadOnlyList<SupportedUniversitiesDto>>(supported);
        }

        public async Task<UniversityDto> GetUniversityByIdAsync(string id)
        {
            var university = await repo.GetByIdAsync(id);
            if (university == null)
            {
                throw new NotFoundException("University not found");
            }

            return mapper.Map<UniversityDto>(university);
        }

        public async Task<UpdateUniversityDto> UpdateUniversityAsync(string id, UpdateUniversityDto updateUniversityDto)
        {
            var university = await repo.GetByIdAsync(id);
            if (university == null)
            {
                throw new NotFoundException("University not found");
            }


            var updatedUniversity = mapper.Map(updateUniversityDto, university);

            await repo.Update(updatedUniversity);
            return mapper.Map<UpdateUniversityDto>(updatedUniversity);
        }
    }
}