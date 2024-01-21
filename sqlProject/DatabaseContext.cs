using Microsoft.EntityFrameworkCore;
using sqlProject.model;

namespace sqlProject
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<Test> Tests { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Answer> Answers { get; set; } =null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserType> UserTypes { get; private set; } = null!;
        public DbSet<Logging> Logging { get; set; } = null!;
        public DbSet<LoggingType> LoggingTypes { get; private set; } = null!;
        public DbSet<AchievementLogging> AchievementLogging { get; private set; } = null!;
        public DbSet<AchievementLoggingAnswer> AchievementLoggingAnswer { get; private set; } = null!;

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserType>().HasData([new(1, "Учитель"), new(2, "Студент")]);
            modelBuilder.Entity<LoggingType>().HasData([new(1, "Вход"), new(2, "Выход")]);
            modelBuilder.Entity<User>().HasData(new User("admin", "admin", 1) { ID = 1 });
        }
    }
}
