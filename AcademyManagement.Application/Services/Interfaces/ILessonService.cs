using System.Threading.Tasks;
using AcademyManagement.Application.DTOs.Lesson;
using AcademyManagement.Domain.Entities.Lesson;

namespace AcademyManagement.Application.Services.Interfaces
{
    public interface ILessonService
    {
        Task<FilterLessonDTO> FilterLessons(FilterLessonDTO filter);
        Task<Lesson> GetLessonById(int lessonId);
        Task<AddLessonResult> AddLesson(AddOrEditLessonDTO addLesson);
        Task<EditLessonResult> EditLesson(AddOrEditLessonDTO editLesson);
        Task<DeleteLessonResult> DeleteLesson(int lessonId);
        Task<bool> IsExistLessonByName(string name);
    }
}