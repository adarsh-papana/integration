using System.Security.Cryptography.X509Certificates;

namespace DigitalBookStoreManagement.Exception
{
    public class NotFoundException : IOException
    {
        public NotFoundException(string message) : base(message) { }
       
    }
}
