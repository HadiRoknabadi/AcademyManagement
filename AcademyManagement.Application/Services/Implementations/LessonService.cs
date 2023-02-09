using AcademyManagement.Application.DTOs.Lesson;
using AcademyManagement.Application.DTOs.Paging;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Application.Services.Interfaces.Contexts;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Application.Services.Implementations
{
    public class LessonService : ILessonService
    {
        #region Constructor

        private readonly IDatabaseContext _context;

        public LessonService(IDatabaseContext context)
        {
            _context = context;
        }


        #endregion

        #region  Filter Lessons

        public async Task<FilterLessonDTO> FilterLessons(FilterLessonDTO filter)
        {
            var query = _context.Lessons.AsQueryable().AsNoTracking();

            #region Order

            switch (filter.OrderBy)
            {
                case FilterLessonOrder.CreateDate_ASC:
                    query = query.OrderBy(u=>EF.Property<DateTime>(u,"InsertTime"));
                    break;

                case FilterLessonOrder.CreateDate_DES:
                    query = query.OrderByDescending(u=>EF.Property<DateTime>(u,"InsertTime"));
                    break;
            }

            #endregion

            #region Filter

            if (!string.IsNullOrEmpty(filter.LessonName))
                query = query.Where(u => EF.Functions.Like(u.Name, $"%{filter.LessonName}%"));

            if (filter.SeasonCount!=0)
                query = query.Where(l=>l.Season_Count==filter.SeasonCount);

            #endregion

            #region Paging

            var allEntitiesCount = await query.CountAsync();

            var pager = Pager.Build(filter.PageId, allEntitiesCount, filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetPaging(pager).SetLessons(allEntities);
        }


        #endregion
    }
}