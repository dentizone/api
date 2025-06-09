namespace Dentizone.Domain.Exceptions;

public class TokenConfigurationException : Exception
{
    public TokenConfigurationException(string message) : base(message)
    {
    }

    public TokenConfigurationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}