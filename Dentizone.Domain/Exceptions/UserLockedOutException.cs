namespace Dentizone.Application.Services.Authentication;

public class UserLockedOutException(string message) : Exception(message);