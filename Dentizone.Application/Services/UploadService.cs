using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using Dentizone.Application.DTOs.Asset;
using Dentizone.Application.Interfaces;
using Dentizone.Application.Interfaces.Asset;
using Dentizone.Application.Interfaces.Cloudinary;
using Microsoft.AspNetCore.Http;

namespace Dentizone.Application.Services
{
  public class UploadService:IUploadService
    {
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IAssetService _assetService;
        private readonly IMapper _mapper;

        public UploadService(ICloudinaryService cloudinaryService, IAssetService assetService,IMapper mapper)
        {
            _cloudinaryService = cloudinaryService;
            _assetService = assetService;
            _mapper = mapper;
        }

        public  async Task<bool> getAssetById(string id)
        {
            var Uploaded_image = await  _assetService.GetAssetByIdAsync(id);
            if (Uploaded_image == null)
            {
                return false;
            }
            return true;
        }

        public async Task<AssetDto> UploadImageAsync(IFormFile file,string FileName)
        {
            var stream = file.OpenReadStream();
            string URL = _cloudinaryService.Upload(stream, FileName);
           var added_image_to_our_DB= await _assetService.CreateAssetAsync(new CreateAssetDto
           {
               Size = file.Length,
               Url = URL,
              Type = Domain.Enums.AssetType.Image
              
           });

            return  added_image_to_our_DB;
        }



      
    }
    }
