namespace Kindergarten.Application.Exceptions
{
    public class AlreadyDeleteException : Exception
    {
        private const string _message = "Already Delete";

        public AlreadyDeleteException(Exception innerException) 
        :base(_message,innerException)
        { 
        }
    }
}
