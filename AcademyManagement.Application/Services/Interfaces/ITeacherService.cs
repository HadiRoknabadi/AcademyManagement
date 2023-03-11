using AcademyManagement.Application.DTOs.Term;
using AcademyManagement.Domain.Entities.Account;

namespace AcademyManagement.Application.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<List<TeachersForTermDTO>> GetTeachersForTerm();
    }
}