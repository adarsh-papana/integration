using DigitalBookStoreManagement.Model;

namespace DigitalBookStoreManagement.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly UserContext context;

        public UserRepo(UserContext context)
        {
            this.context = context;
        }
        public List<User> GetUserInfo()
        {
            return context.Users.ToList();

        }

        public User GetUserInfo(int id)
        {
            return context.Users.FirstOrDefault(x => x.UserID == id);
        }

        public int AddUser(User userInfo)
        {
            context.Users.Add(userInfo);
            return context.SaveChanges();
        }

        public int RemoveUser(int id)
        {
            User UI = context.Users.FirstOrDefault(e => e.UserID == id);
            context.Users.Remove(UI);
            return context.SaveChanges();
        }

        public int UpdateUser(int id, User userInfo)
        {
            User UI = context.Users.Where(x => x.UserID == id).FirstOrDefault();
            UI.UserID = userInfo.UserID;
            UI.Name = userInfo.Name;
            UI.Email = userInfo.Email;
            UI.Password = userInfo.Password;
            UI.ConfirmPassword = userInfo.ConfirmPassword;
            UI.Address = userInfo.Address;
            UI.Role = userInfo.Role;
            context.Entry<User>(UI).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return context.SaveChanges();

        }
    }
}
