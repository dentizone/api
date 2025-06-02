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
    internal class UniversityService : IUniversity_service
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
            var deleteTask = await _repo.GetByIdAsync(id);
            if (deleteTask == null)
            {
                throw new ArgumentException("University not found");
            }
            deleteTask.IsSupported = false;
            return await _repo.Update(deleteTask) != null;
        }


        public async Task<ICollection<SupportedUniversitiesDTO>> GetSupportedUniversitiesAsync()
        {
            var universities = await _repo.GetAll();
            var supported = universities.Where(u => u.IsSupported).ToList();
            return _mapper.Map<ICollection<SupportedUniversitiesDTO>>(supported);
        }
        public async Task<bool> UpdateUniversityAsync(string id, bool IsSupported)
        {
            var university = await _repo.GetByIdAsync(id);
            if (university == null)
            {
                throw new ArgumentException("University not found");
            }
            university.IsSupported = IsSupported;
            await _repo.Update(university);
            return true;
        }
    }
}

