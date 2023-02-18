using AcademyManagement.Domain.Attributes;
using AcademyManagement.Domain.Entities.Term;

namespace AcademyManagement.Domain.Entities.Lesson
{
    [Auditable]
    public class Lesson
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public int Season_Count { get; set; }
        public string Lesson_File { get; set; }

        #endregion

        #region Relations

        public ICollection<TermLesson> TermLessons { get; set; }

        #endregion
    }
}
