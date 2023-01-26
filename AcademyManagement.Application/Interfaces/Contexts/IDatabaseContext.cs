using AcademyManagement.Domain.Entities.Account;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Application.Interfaces.Contexts
{
    public interface IDatabaseContext
    {

        #region Account

        public DbSet<PreRegisteration> PreRegisterations { get; set; }

        #endregion

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
