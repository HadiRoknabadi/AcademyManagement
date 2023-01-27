using AcademyManagement.Application.Services.Interfaces.Contexts;
using AcademyManagement.Domain.Attributes;
using AcademyManagement.Domain.Entities.Account;
using AcademyManagement.Persistence.Configs.PreRegisteration;
using AcademyManagement.Persistence.Configs.User;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Persistence.Contexts
{
    public class DataBaseContext:DbContext,IDatabaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {

        }


        #region Account

        public DbSet<PreRegisteration> PreRegisterations { get; set; }

        #endregion


        protected override void OnModelCreating(ModelBuilder builder)
        {

            GetEntitiesConfigs(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if(entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute),true).Length > 0)
                {
                    builder.Entity(entityType.Name).Property<DateTime?>("InsertTime");
                    builder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                    builder.Entity(entityType.Name).Property<DateTime?>("RemovedTime");
                    builder.Entity(entityType.Name).Property<bool>("IsRemoved");
                }
            }
            

            base.OnModelCreating(builder);
        }


        private static void GetEntitiesConfigs(ModelBuilder modelBuilder)
        {
            #region Account

            modelBuilder.ApplyConfiguration(new PreRegisterationConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            #endregion
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified || p.State == EntityState.Added || p.State == EntityState.Deleted);

            foreach (var entity in modifiedEntries)
            {
                var entityType = entity.Context.Model.FindEntityType(entity.Entity.GetType());

                var inserted = entityType.FindProperty("InsertTime");
                var updateTime = entityType.FindProperty("UpdateTime");
                var RemoveTime = entityType.FindProperty("RemoveTime");
                var IsRemoved = entityType.FindProperty("IsRemoved");

                if(entity.State==EntityState.Added && inserted!=null)
                {
                    entity.Property("InsertTime").CurrentValue = DateTime.Now;
                }

                if (entity.State == EntityState.Modified && updateTime != null)
                {
                    entity.Property("UpdateTime").CurrentValue = DateTime.Now;
                }

                if (entity.State == EntityState.Deleted && RemoveTime != null && IsRemoved!=null)
                {
                    entity.Property("RemoveTime").CurrentValue = DateTime.Now;
                    entity.Property("IsRemoved").CurrentValue = true;
                }
            }


            return base.SaveChanges();
        }

    }
}
