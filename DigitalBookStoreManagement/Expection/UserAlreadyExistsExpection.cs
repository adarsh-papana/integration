namespace DigitalBookStoreManagement.Expection
{
    public class UserAlreadyExistsExpection : ApplicationException
    {
        public UserAlreadyExistsExpection() { }
        public UserAlreadyExistsExpection(string msg) : base(msg) { }
    }
}
