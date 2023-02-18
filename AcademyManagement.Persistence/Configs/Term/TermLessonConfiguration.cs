using AcademyManagement.Domain.Entities.Term;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyManagement.Persistence.Configs.Term
{
    public class TermLessonConfiguration : IEntityTypeConfiguration<TermLesson>
    {
        public void Configure(EntityTypeBuilder<TermLesson> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(l=>l.Term).WithMany(l=>l.TermLessons).HasForeignKey(l=>l.Lesson_Id);

            builder.HasOne(l=>l.Lesson).WithMany(l=>l.TermLessons).HasForeignKey(l=>l.Lesson_Id);
        }
    }
}
