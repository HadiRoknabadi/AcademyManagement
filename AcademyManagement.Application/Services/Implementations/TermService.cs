using AcademyManagement.Application.DTOs.Paging;
using AcademyManagement.Application.DTOs.Term;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Application.Services.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Application.Services.Implementations
{
    public class TermService : ITermService
    {
        #region Constructor

        private readonly IDatabaseContext _cotnext;

        public TermService(IDatabaseContext cotnext)
        {
            _cotnext = cotnext;
        }


        #endregion


        #region Filter Terms

        public async Task<FilterTermDTO> FilterTerms(FilterTermDTO filter)
        {
            var query = _cotnext.Terms.AsQueryable().AsNoTracking();

            #region Filter

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(u => EF.Functions.Like(u.TermName, $"%{filter.Name}%"));

            #endregion

            #region Paging

            var allEntitiesCount = await query.CountAsync();

            var pager = Pager.Build(filter.PageId, allEntitiesCount, filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetPaging(pager).SetTerms(allEntities);
        }

        #endregion
    }
}