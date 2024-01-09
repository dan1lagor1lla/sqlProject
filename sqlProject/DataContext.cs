using Microsoft.EntityFrameworkCore;
using sqlProject.model;

namespace sqlProject
{
    internal class DataContext : DbContext
    {
        public DbSet<Test> Tests { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserType> UserTypes { get; private set; } = null!;
        public DbSet<Logging> Logging { get; set; } = null!;
        public DbSet<LoggingType> LoggingTypes { get; private set; } = null!;
        public DbSet<AchievementLogging> AchievementLogging { get; private set; } = null!;

        public DataContext() { Database.EnsureCreated(); }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserType>().HasData(UserType.Teacher, UserType.Student);
            modelBuilder.Entity<LoggingType>().HasData(LoggingType.Login, LoggingType.Logout);
        }
    }
}
