namespace Dentizone.Domain.Exceptions
{
    public abstract class BaseException : Exception
    {
        protected BaseException(string message) : base(message)
        {
        }
    }

    public class NotFoundException : BaseException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    public class BadActionException : BaseException
    {
        public BadActionException(string message) : base(message)
        {
        }
    }

    public class UserLockedOutException : BaseException
    {
        public UserLockedOutException(string message) : base(message)
        {
        }
    }

    public class UserAlreadyExistsException : BaseException
    {
        public UserAlreadyExistsException(string message) : base(message)
        {
        }
    }
}