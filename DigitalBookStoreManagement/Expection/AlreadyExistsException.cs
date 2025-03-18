namespace DigitalBookStoreManagement.Exception
{
    public class AlreadyExistsException : IOException
    {
        public AlreadyExistsException(string message) : base(message) { }
    }
}
