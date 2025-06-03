namespace Dentizone.Domain.Exceptions
{
    public class CloudinaryUploadException : ApplicationException
    {
        public CloudinaryUploadException(string message) : base(message)
        {
        }
    }
}