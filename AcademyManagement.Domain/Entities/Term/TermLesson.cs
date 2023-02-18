using AcademyManagement.Domain.Attributes;
using AcademyManagement.Domain.Entities.Account;

namespace AcademyManagement.Domain.Entities.Term
{
    [Auditable]
    public class TermLesson
    {
        #region Properties
        public int Id { get; set; }
        public int Term_Id { get; set; }
        public int Lesson_Id { get; set; }
        public string Teacher_Id { get; set; }

        #endregion

        #region Relations

        public Term Term { get; set; }
        public AcademyManagement.Domain.Entities.Lesson.Lesson Lesson { get; set; }

        #endregion
    }
}
