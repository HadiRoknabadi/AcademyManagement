using AcademyManagement.Application.DTOs.Common;
using AcademyManagement.Application.DTOs.Lesson;
using AcademyManagement.Application.DTOs.Paging;
using AcademyManagement.Application.Generators;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Application.Services.Interfaces.Contexts;
using AcademyManagement.Domain.Entities.Lesson;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Application.Services.Implementations
{
    public class LessonService : ILessonService
    {
        #region Constructor

        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUploader _uploader;

        public LessonService(IDatabaseContext context, IMapper mapper, IUploader uploader)
        {
            _context = context;
            _mapper = mapper;
            _uploader = uploader;
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

        #region Add Lesson

        public async Task<AddLessonResult> AddLesson(AddOrEditLessonDTO addLesson)
        {
            if(await IsExistLessonByName(addLesson.Name)) return AddLessonResult.ExistLesson;

            var lesson=_mapper.Map<AddOrEditLessonDTO,Lesson>(addLesson);

            if(addLesson.PdfFile!=null)
            {
                var imageName=Generator.GenerateUniqCode() + Path.GetExtension(addLesson.PdfFile.FileName);

                var res=await _uploader.UploadPdf(UploadFileType.PDF,addLesson.PdfFile,imageName);

                switch(res)
                {
                    case DTOs.Common.UploadResult.Success:
                        lesson.Lesson_File = imageName;
                        break;

                    case DTOs.Common.UploadResult.CantUploadFile:
                        return AddLessonResult.CantUploadFile;
                }

            }

            await _context.Lessons.AddAsync(lesson);

            await _context.SaveChangesAsync();

            return AddLessonResult.Success;

        }
        

        #endregion

        public async Task<bool> IsExistLessonByName(string name)
        {
            return await _context.Lessons.AsQueryable().AsNoTracking().AnyAsync(l=>l.Name==name);
        }

    }
}