using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs;

namespace Dentizone.Application.Interfaces
{
    internal interface IUniversity_service
    {
        Task<ICollection<SupportedUniversitiesDTO>> GetSupportedUniversitiesAsync();
        Task<bool>DeleteUniversity(string id);
        Task<CreateUniversityDTO> CreateUniversityAsync(CreateUniversityDTO universityDto);
        Task<bool> UpdateUniversityAsync(string id,bool IsSupported);
    }
}
