using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Mapping;

namespace Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserModel> UsersData { get; set; }
        
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            base.OnModelCreating(modelBuilder);
        }
    }
}