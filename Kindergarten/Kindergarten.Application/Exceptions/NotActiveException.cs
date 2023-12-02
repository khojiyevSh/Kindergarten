namespace Kindergarten.Application.Exceptions
{
    public class NotActiveException : Exception
    {
        private const string _message = "Not Active";

        public NotActiveException() 
        :base(_message)
        { 
        }
    }
}
