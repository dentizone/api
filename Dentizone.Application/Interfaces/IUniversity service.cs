using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs;

namespace Dentizone.Application.Interfaces
{
    internal interface IUniversityService
    {
        Task<ICollection<SupportedUniversitiesDTO>> GetSupportedUniversitiesAsync();
        Task<bool>DeleteUniversity(string id);
        Task<CreateUniversityDTO> CreateUniversityAsync(CreateUniversityDTO universityDto);
        Task<UpdateUniversityDTO> UpdateUniversityAsync(string id,UpdateUniversityDTO updateUniversityDTO);
    }
}
