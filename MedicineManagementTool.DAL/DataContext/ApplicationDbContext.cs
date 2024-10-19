using MedicineManagementTool.DAL.Entity;
using Microsoft.EntityFrameworkCore;

namespace MedicineManagementTool.DAL.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(a => new { a.UserId, a.RoleId });

            modelBuilder.Entity<Role>().HasData(
                new Role() { RoleId = 1, RoleName = "Admin", },
                new Role() { RoleId = 2, RoleName = "User" }
               );

            modelBuilder.Entity<User>().HasData(
                new User() {  Id= 1, Name = "Nitesh",Email = "nitesh@gmail.com",Password= "zYKkj0RmEJJ7dw6acXM5SQ==" }   //Nn@1314             
               );

            modelBuilder.Entity<UserRole>().HasData(
                new UserRole() { RoleId = 1,UserId = 1}
                );
        }

        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<SaleDetails> SaleDetails { get; set; }

    }
}
