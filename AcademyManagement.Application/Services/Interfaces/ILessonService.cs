using AcademyManagement.Application.DTOs.Lesson;

namespace AcademyManagement.Application.Services.Interfaces
{
    public interface ILessonService
    {
        Task<FilterLessonDTO> FilterLessons(FilterLessonDTO filter);
    }
}