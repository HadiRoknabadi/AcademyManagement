using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyManagement.Persistence.Congigs.PreRegisteration
{
    public class PreRegisterationConfiguration : IEntityTypeConfiguration<AcademyManagement.Domain.Entities.Account.PreRegisteration>
    {
        public void Configure(EntityTypeBuilder<AcademyManagement.Domain.Entities.Account.PreRegisteration> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name).IsRequired().HasMaxLength(150);

            builder.Property(p => p.Family).IsRequired().HasMaxLength(150);

            builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(50);

            builder.Property(p => p.Grade).IsRequired().HasMaxLength(300);

            builder.Property(p => p.HowManyTermPassed).IsRequired(false);

            builder.Property(p => p.BookNameReadInEnglishClass).IsRequired(false).HasMaxLength(300);

            builder.Property(p => p.Description).IsRequired(false).HasMaxLength(300);
        }
    }
}
