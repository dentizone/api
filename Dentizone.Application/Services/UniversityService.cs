using AutoMapper;
using Dentizone.Application.DTOs.University;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;
using Dentizone.Domain.Exceptions;

namespace Dentizone.Application.Services
{
    internal class UniversityService : IUniversityService
    {
        private readonly IMapper _mapper;
        private readonly IUniversityRepository _repo;

        public UniversityService(IMapper mapper, IUniversityRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<CreateUniversityDto> CreateUniversityAsync(CreateUniversityDto universityDto)
        {
            var newUniversity = await _repo.CreateAsync(_mapper.Map<University>(universityDto));
            return _mapper.Map<CreateUniversityDto>(newUniversity);
        }

        public async Task<UniversityDto> DeleteUniversity(string id)
        {
            var deleted = await _repo.DeleteAsync(id);

            return _mapper.Map<UniversityDto>(deleted) ?? throw new NotFoundException("No University found with this id. Please check the id and try again.")
        }


        public async Task<IReadOnlyList<SupportedUniversitiesDto>> GetSupportedUniversitiesAsync()
        {
            var universities = await _repo.GetAll();
            var supported = universities.Where(u => u.IsSupported).ToList();
            return _mapper.Map<IReadOnlyList<SupportedUniversitiesDto>>(supported);
        }

        public async Task<UpdateUniversityDto> UpdateUniversityAsync(string id, UpdateUniversityDto updateUniversityDto)
        {
            var university = await _repo.GetByIdAsync(id);
            if (university == null)
            {
                throw new NotFoundException("University not found");
            }


            var updatedUniversity = _mapper.Map(updateUniversityDto, university);

            await _repo.Update(updatedUniversity);
            return _mapper.Map<UpdateUniversityDto>(updatedUniversity);
        }
    }
}