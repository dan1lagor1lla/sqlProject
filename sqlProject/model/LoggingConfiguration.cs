using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sqlProject.model
{
    internal class LoggingConfiguration : IEntityTypeConfiguration<Logging>
    {
        public void Configure(EntityTypeBuilder<Logging> builder)
        {
            builder.Property("Date").HasField("date");
            builder.Property("Time").HasField("time");
        }
    }
}
