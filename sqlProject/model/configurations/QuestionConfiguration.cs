using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace sqlProject.model.configurations
{
    internal class QuestionConfiguration : IEntityTypeConfiguration<Question> 
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property("Content").HasField("content");
            builder.Property("IsUsing").HasField("isUsing");
        }
    }
}
