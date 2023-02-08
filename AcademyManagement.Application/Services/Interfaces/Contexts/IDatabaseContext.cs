﻿using AcademyManagement.Domain.Entities.Account;
using AcademyManagement.Domain.Entities.Lesson;
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

        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
