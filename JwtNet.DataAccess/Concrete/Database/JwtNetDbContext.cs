
using JwtNet.Entities.DbModels;
using Microsoft.EntityFrameworkCore;

namespace JwtNet.DataAccess.Concrete.EFCore.Database
{
    public class JwtNetDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=JwtNetDb; User Id=sa; Password=sa@123");
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
