using AcademyManagement.Domain.Attributes;

namespace AcademyManagement.Domain.Entities.Lesson
{
    [Auditable]
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Season_Count { get; set; }
        public string Lesson_File { get; set; }
    }
}
