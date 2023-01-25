using AcademyManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Application.Interfaces.Contexts
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
