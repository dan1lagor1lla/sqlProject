using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace sqlProject.model
{
    internal class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.Property("Name").HasField("name");
            builder.Property("IsQuestionsOrderRandom").HasField("isQuestionsOrderRandom");
        }
    }
}
