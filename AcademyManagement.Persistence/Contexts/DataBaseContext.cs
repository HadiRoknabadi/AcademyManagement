using AcademyManagement.Application.Interfaces.Contexts;
using AcademyManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Persistence.Contexts
{
    public class DataBaseContext:DbContext,IDatabaseContext
    {
        public DataBaseContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }

    }
}
