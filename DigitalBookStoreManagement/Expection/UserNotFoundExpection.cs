namespace DigitalBookStoreManagement.Expection
{
    public class UserNotFoundExpection : ApplicationException
    {
        public UserNotFoundExpection() { }
        public UserNotFoundExpection(string msg) : base(msg) { }
    }
}
