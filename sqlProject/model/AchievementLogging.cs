using Microsoft.EntityFrameworkCore;

namespace sqlProject.model
{
    [EntityTypeConfiguration(typeof(AchievementLoggingConfiguration))]
    internal class AchievementLogging
    {
        private readonly DateOnly date;
        private readonly TimeOnly startTime;
        private TimeOnly? endTime;
        private bool isOver;
        
        public int ID { get; set; }
        public int StudentID { get; set; }
        public User Student { get; set; }
        public int TestID { get; set; }
        public Test Test { get; set; }
        public DateOnly Date => date;
        public TimeOnly StartTime => startTime;
        public TimeOnly? EndTime => endTime;
        public List<Answer> Answers { get; private set; } = new();

        public AchievementLogging()
        {
            date = DateOnly.FromDateTime(DateTime.Now);
            startTime = TimeOnly.FromDateTime(DateTime.Now);
            isOver = false;
        }
        public AchievementLogging(User student, Test test) : this()
        {
            Test = test;
            Student = student;
        }

        public void Finish()
        {
            if (isOver)
                return;
            isOver = true;
            endTime = TimeOnly.FromDateTime(DateTime.Now);
        }
    }
}
