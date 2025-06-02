using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dentizone.Application.DTOs;
using Dentizone.Application.Interfaces;
using Dentizone.Domain.Entity;

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

        public async Task<CreateUniversityDTO> CreateUniversityAsync(CreateUniversityDTO universityDto)
        {
            var newUniversity = await _repo.CreateAsync(_mapper.Map<University>(universityDto));
            return _mapper.Map<CreateUniversityDTO>(newUniversity);
        }
        public async Task<bool> DeleteUniversity(string id)
        {
            
            var deletedTask = await _repo.DeleteAsync(id);
            return deletedTask != null && !deletedTask.IsDeleted;
        }


        public async Task<ICollection<SupportedUniversitiesDTO>> GetSupportedUniversitiesAsync()
        {
            var universities = await _repo.GetAll();
            var supported = universities.Where(u => u.IsSupported).ToList();
            return _mapper.Map<ICollection<SupportedUniversitiesDTO>>(supported);
        }
        public async Task<UpdateUniversityDTO> UpdateUniversityAsync(string id,UpdateUniversityDTO newUniversityDTO)
        {
            var university = await _repo.GetByIdAsync(id);
            if (university == null)
            {
                throw new ArgumentException("University not found");
            }
            university.IsSupported =newUniversityDTO.IsSupported;
            university.Name = newUniversityDTO.Name;
            await _repo.Update(university);
            return _mapper.Map<UpdateUniversityDTO>(university);
        }

    }
}

