using AcademyManagement.Domain.Attributes;
using AcademyManagement.Domain.Entities.Term;
using Microsoft.AspNetCore.Identity;

namespace AcademyManagement.Domain.Entities.Account
{
    [Auditable]
    public class User : IdentityUser
    {
        #region Properties

        public string Name { get; set; }
        public string Family { get; set; }
        public string Avatar { get; set; }
        public string FullName { get { return $"{Name} {Family}"; } }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime RemovedTime { get; set; }
        public bool IsRemoved { get; set; }

        #endregion

        #region Relations

        public ICollection<TermLesson> TermLessons { get; set; }
        public ICollection<TermUser> TermUsers { get; set; }

        #endregion
    }
}
