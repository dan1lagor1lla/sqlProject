using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sqlProject.model
{
    internal class AchievementLoggingConfiguration : IEntityTypeConfiguration<AchievementLogging>
    {
        public void Configure(EntityTypeBuilder<AchievementLogging> builder)
        {
            builder.Property("Date").HasField("date");
            builder.Property("StartTime").HasField("startTime");
            builder.Property("EndTime").HasField("endTime");
            builder.HasMany(l => l.Answers).WithMany("Loggings").UsingEntity<AchievementLoggingAnswer>();
        }
    }
}
