using Microsoft.AspNetCore.Http;

namespace AcademyManagement.Application.DTOs.Lesson
{
    public class AddOrEditLessonDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public int Season_Count { get; set; }
        public IFormFile PdfFile { get; set; }
    }

    public enum AddLessonResult
    {
        Success,
        ExistLesson,
        CantUploadFile
    }
    public enum EditLessonResult
    {
        Success,
        CantUploadFile,
        ExistLesson,
        NotFound
    }
}