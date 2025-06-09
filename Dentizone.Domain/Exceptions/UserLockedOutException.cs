namespace Dentizone.Domain.Exceptions;

public class UserLockedOutException(string message) : Exception(message);