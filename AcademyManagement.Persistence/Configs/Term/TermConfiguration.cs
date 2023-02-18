using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Persistence.Configs.Term
{
    public class TermConfiguration : IEntityTypeConfiguration<AcademyManagement.Domain.Entities.Term.Term>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AcademyManagement.Domain.Entities.Term.Term> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(t => t.TermName).IsRequired().HasMaxLength(200);

            
        }
    }
}
