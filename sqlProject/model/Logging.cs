using Microsoft.EntityFrameworkCore;

namespace sqlProject.model
{
    [EntityTypeConfiguration(typeof(LoggingConfiguration))]
    internal class Logging
    {
        private readonly DateOnly date;
        private readonly TimeOnly time;

        public int ID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public DateOnly Date => date;
        public TimeOnly Time => time;
        public int LoggingTypeID { get; set; }
        public LoggingType LoggingType { get; set; }
        
        private Logging()
        {
            date = DateOnly.FromDateTime(DateTime.Now);
            time = TimeOnly.FromDateTime(DateTime.Now);
        }
        public Logging(User user, LoggingType type) : this()
        {
            User = user;
            LoggingType = type;
        }
    }
}
