namespace Kindergarten.Application.Exceptions
{
    public class NotFoundException :Exception
    {
        private const string _message = "Not Faund";

        public NotFoundException()
            :base(_message) 
        {
        }
    }
}
