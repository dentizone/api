using Dentizone.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dentizone.Infrastructure.Persistence.Configurations
{
    internal class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.QuestionId)
                .IsRequired();

            builder.Property(a => a.Text)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(a => a.Status)
                .IsRequired();

            builder.Property(w => w.CreatedAt)
                .HasDefaultValueSql(SqlCommon.Date)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(a => a.Question)
                .WithOne(q => q.Answer)
                .HasForeignKey<Answer>(a => a.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}