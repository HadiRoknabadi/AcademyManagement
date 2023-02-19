using AcademyManagement.Application.DTOs.Term;

namespace AcademyManagement.Application.Services.Interfaces
{
    public interface ITermService
    {
        Task<FilterTermDTO> FilterTerms(FilterTermDTO filter);
    }
}