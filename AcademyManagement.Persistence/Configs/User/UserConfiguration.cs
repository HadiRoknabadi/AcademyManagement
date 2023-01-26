using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcademyManagement.Persistence.Configs.User
{
    public class UserConfiguration : IEntityTypeConfiguration<AcademyManagement.Domain.Entities.Account.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Account.User> builder)
        {
            builder.Property(u => u.Name).IsRequired().HasMaxLength(200);

            builder.Property(u => u.Family).IsRequired().HasMaxLength(200);

            builder.Ignore(u => u.FullName);
        }
    }
}
