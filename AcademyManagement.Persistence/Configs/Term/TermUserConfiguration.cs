using AcademyManagement.Domain.Entities.Term;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyManagement.Persistence.Configs.Term
{
    public class TermUserConfiguration : IEntityTypeConfiguration<TermUser>
    {
        public void Configure(EntityTypeBuilder<TermUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(u=>u.Term).WithMany(u=>u.TermUsers).HasForeignKey(u=>u.TermId);

           

        }
    }
}
