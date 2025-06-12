using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dentizone.Application.DTOs.Asset;
using Microsoft.AspNetCore.Http;

namespace Dentizone.Application.Interfaces
{
    public interface IUploadService
    {
        public Task<AssetDto> UploadImageAsync(IFormFile file,string FileName);
    }
}
