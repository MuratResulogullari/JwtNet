using JwtNet.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtNet.WebAPI.Business
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=Application; User Id=sa; Password=sa@123");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
