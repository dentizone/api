namespace Dentizone.Application.Services.Authentication;

public class UserAlreadyExistsException(string message) : Exception(message);