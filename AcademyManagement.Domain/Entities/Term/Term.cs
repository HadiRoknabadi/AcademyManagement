using AcademyManagement.Domain.Attributes;

namespace AcademyManagement.Domain.Entities.Term
{
    [Auditable]
    public class Term
    {
        #region Properties
        public int Id { get; set; }
        public string TermName { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public bool IsTermEnded { get; set; }
        #endregion

        #region Relations

        public ICollection<TermLesson> TermLessons { get; set; }
        public ICollection<TermUser> TermUsers { get; set; }

        #endregion
    }
}
