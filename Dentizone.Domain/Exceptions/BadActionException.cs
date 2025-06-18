namespace Dentizone.Domain.Exceptions
{
    public abstract class BaseException(string message) : Exception(message);

    public class NotFoundException(string message) : BaseException(message);

    public class BadActionException(string message) : BaseException(message);

    public class UserLockedOutException(string message) : BaseException(message);

    public class UserAlreadyExistsException(string message) : BaseException(message);
}