using Microsoft.EntityFrameworkCore;
using MyWebApp.Models.DBEntities;

namespace MyWebApp.DEL
{
    public class UsersDBContext : DbContext
    {
        public UsersDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> UsersData { get; set; }
    }
}
