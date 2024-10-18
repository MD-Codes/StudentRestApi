using Microsoft.EntityFrameworkCore;

namespace StudentRestApi.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().HasData(new Student
            {
                StudentId = 1,
                FirstName = "Adam",
                LastName = "Adam",
                Email = "Adam@test.com",
                Gender = Gender.Male,
                PhotoPath = "Images/Adam.png"

            });
            modelBuilder.Entity<Student>().HasData(new Student
            {
                StudentId = 2,
                FirstName = "Anna",
                LastName = "Anna",
                Email = "Anna@test.com",
                Gender = Gender.Female,
                PhotoPath = "Images/Anna.png"

            });
            modelBuilder.Entity<Student>().HasData(new Student
            {
                StudentId = 3,
                FirstName = "Kris",
                LastName = "Kris",
                Email = "Kris@test.com",
                Gender = Gender.Male,
                PhotoPath = "Images/Kris.png"

            });
            modelBuilder.Entity<Student>().HasData(new Student
            {
                StudentId = 4,
                FirstName = "Filip",
                LastName = "Filip",
                Email = "Filip@test.com",
                Gender = Gender.Male,
                PhotoPath = "Images/Filip.png"

            });
        }

        
    }
}
