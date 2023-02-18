using AcademyManagement.Domain.Entities.Account;
using AcademyManagement.Domain.Entities.Lesson;
using AcademyManagement.Domain.Entities.Term;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Application.Services.Interfaces.Contexts
{
    public interface IDatabaseContext
    {

        #region Account

        public DbSet<PreRegisteration> PreRegisterations { get; set; }

        #endregion

        #region Lesson

        public DbSet<Lesson> Lessons { get; set; }

        #endregion

        #region Term

        public DbSet<Term> Terms { get; set; }
        public DbSet<TermUser> TermUsers { get; set; }
        public DbSet<TermLesson> TermLessons { get; set; }


        #endregion

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
