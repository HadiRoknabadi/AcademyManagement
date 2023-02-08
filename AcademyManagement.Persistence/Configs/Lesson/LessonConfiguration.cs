using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyManagement.Persistence.Configs.Lesson
{
    public class LessonConfiguration : IEntityTypeConfiguration<AcademyManagement.Domain.Entities.Lesson.Lesson>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Lesson.Lesson> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name).IsRequired().HasMaxLength(150);

            builder.Property(l => l.Lesson_File).HasMaxLength(200);

            builder.HasQueryFilter(u => EF.Property<bool>(u, "IsRemoved") == false);

        }
    }
}
