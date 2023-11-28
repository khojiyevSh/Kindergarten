namespace Kindergarten.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message)
        : base($"{message} not found")
        { 
        }
    }
}
