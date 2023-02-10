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
                    query = query.OrderBy(u => EF.Property<DateTime>(u, "InsertTime"));
                    break;

                case FilterLessonOrder.CreateDate_DES:
                    query = query.OrderByDescending(u => EF.Property<DateTime>(u, "InsertTime"));
                    break;
            }

            #endregion

            #region Filter

            if (!string.IsNullOrEmpty(filter.LessonName))
                query = query.Where(u => EF.Functions.Like(u.Name, $"%{filter.LessonName}%"));

            if (filter.SeasonCount != 0)
                query = query.Where(l => l.Season_Count == filter.SeasonCount);

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
            if (await IsExistLessonByName(addLesson.Name)) return AddLessonResult.ExistLesson;

            var lesson = _mapper.Map<AddOrEditLessonDTO, Lesson>(addLesson);

            if (addLesson.PdfFile != null)
            {
                var imageName = Generator.GenerateUniqCode() + Path.GetExtension(addLesson.PdfFile.FileName);

                var res = await _uploader.UploadPdf(UploadFileType.PDF, addLesson.PdfFile, imageName);

                switch (res)
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

        #region Edit Lesson

        public async Task<EditLessonResult> EditLesson(AddOrEditLessonDTO editLesson)
        {
            var lesson = await GetLessonById((int)editLesson.Id);

            if (lesson == null) return EditLessonResult.NotFound;

            if (lesson.Name != editLesson.Name && await IsExistLessonByName(editLesson.Name)) return EditLessonResult.ExistLesson;


            if (editLesson.PdfFile != null)
            {
                var imageName = Generator.GenerateUniqCode() + Path.GetExtension(editLesson.PdfFile.FileName);

                var res = await _uploader.UploadPdf(UploadFileType.PDF, editLesson.PdfFile, imageName,lesson.Lesson_File);

                switch (res)
                {
                    case DTOs.Common.UploadResult.Success:
                        lesson.Lesson_File = imageName;
                        break;

                    case DTOs.Common.UploadResult.CantUploadFile:
                        return EditLessonResult.CantUploadFile;
                }

            }

            var editedLesson = _mapper.Map<AddOrEditLessonDTO, Lesson>(editLesson, lesson);

            _context.Lessons.Update(editedLesson);

            await _context.SaveChangesAsync();

            return EditLessonResult.Success;
        }

        #endregion

        #region Delete Lesson

        public async Task<DeleteLessonResult> DeleteLesson(int lessonId)
        {
            var lesson=await GetLessonById(lessonId);

            if(lesson==null) return DeleteLessonResult.NotFound;

            _context.Lessons.Remove(lesson);
            
            await _context.SaveChangesAsync();

            return DeleteLessonResult.Success;

        }


        #endregion

        public async Task<bool> IsExistLessonByName(string name)
        {
            return await _context.Lessons.AsQueryable().AsNoTracking().AnyAsync(l => l.Name == name);
        }

        public async Task<Lesson> GetLessonById(int lessonId)
        {
            return await _context.Lessons.AsQueryable().SingleOrDefaultAsync(l => l.Id == lessonId);
        }


    }
}