namespace Dentizone.Application.Interfaces;

public interface ICloudinaryService
{
    string Upload(Stream fileStream, string fileName);
}