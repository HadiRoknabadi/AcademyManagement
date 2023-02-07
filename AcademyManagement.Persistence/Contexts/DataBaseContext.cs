using AcademyManagement.Application.Services.Interfaces.Contexts;
using AcademyManagement.Domain.Attributes;
using AcademyManagement.Domain.Entities.Account;
using AcademyManagement.Persistence.Configs.PreRegisteration;
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
                    builder.Entity(entityType.Name).Property<DateTime>("InsertTime");
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


            #endregion
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
             var modifiedEntries = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified || p.State == EntityState.Added || p.State == EntityState.Deleted);

            foreach (var entity in modifiedEntries)
            {
                var entityType = entity.Context.Model.FindEntityType(entity.Entity.GetType());

                var inserted = entityType.FindProperty("InsertTime");
                var updateTime = entityType.FindProperty("UpdateTime");
                var IsRemoved = entityType.FindProperty("IsRemoved");
                var RemovedTime = entityType.FindProperty("RemovedTime");


                if(entity.State==EntityState.Added && inserted!=null)
                {
                    entity.Property("InsertTime").CurrentValue = DateTime.Now;
                }

                if (entity.State == EntityState.Modified && updateTime != null)
                {
                    entity.Property("UpdateTime").CurrentValue = DateTime.Now;
                }

                if (entity.State == EntityState.Deleted && RemovedTime != null && IsRemoved!=null)
                {
                    entity.Property("RemovedTime").CurrentValue = DateTime.Now;
                    entity.Property("IsRemoved").CurrentValue = true;
                    entity.State = EntityState.Modified;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }


    }
}
