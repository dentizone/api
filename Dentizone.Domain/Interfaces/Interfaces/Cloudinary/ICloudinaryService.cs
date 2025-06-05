namespace Dentizone.Application.Interfaces.Cloudinary;

public interface ICloudinaryService
{
    string Upload(Stream fileStream, string fileName);
}