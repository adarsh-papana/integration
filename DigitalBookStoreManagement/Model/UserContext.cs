using Microsoft.EntityFrameworkCore;

namespace DigitalBookStoreManagement.Model
{

        public class UserContext : DbContext
        {
            public UserContext() { }
            public UserContext(DbContextOptions options) : base(options)
            {
                Database.EnsureCreated();
            }

            public DbSet<User> Users { get; set; }
        }
    
}
