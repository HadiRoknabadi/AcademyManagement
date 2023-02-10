using AcademyManagement.Application.DTOs.Lesson;

namespace AcademyManagement.Application.Services.Interfaces
{
    public interface ILessonService
    {
        Task<FilterLessonDTO> FilterLessons(FilterLessonDTO filter);

        Task<AddLessonResult> AddLesson(AddOrEditLessonDTO addLesson);
        Task<bool> IsExistLessonByName(string name);
    }
}