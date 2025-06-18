using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dentizone.Application.Interfaces.Cloudinary;
using Dentizone.Domain.Exceptions;
using Dentizone.Domain.Interfaces.Secret;

namespace Dentizone.Application.Services
{
    public class CloudinaryService(ISecretService secretService) : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary = new(secretService.GetSecret("cloudinary"));


        public string Upload(Stream fileStream, string fileName)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, fileStream)
            };
            var uploadResult = _cloudinary.Upload(uploadParams);
            if (uploadResult.Error != null)
            {
                throw new CloudinaryUploadException($"Error uploading image: {uploadResult.Error.Message}");
            }

            return uploadResult.SecureUri.ToString();
        }
    }
}