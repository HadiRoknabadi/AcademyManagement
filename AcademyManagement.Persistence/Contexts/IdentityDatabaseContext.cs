using AcademyManagement.Domain.Entities.Account;
using AcademyManagement.Persistence.Configs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Persistence.Contexts
{
    public class IdentityDatabaseContext:IdentityDbContext<User,IdentityRole,string>
    {
        public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options):base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            GetEntitiesConfigs(builder);


            builder.Entity<IdentityUser<string>>().ToTable("Users", "Identity");
            builder.Entity<IdentityRole<string>>().ToTable("Roles", "Identity");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Identity");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Identity");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Identity");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Identity");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Identity");

            builder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });

            builder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });

            builder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.LoginProvider, p.Name });
            
            builder.Entity<IdentityUser<string>>().HasQueryFilter(u=>EF.Property<bool>(u,"IsRemoved")==false);
            
        }

        private static void GetEntitiesConfigs(ModelBuilder modelBuilder)
        {
         
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }

    }
}
