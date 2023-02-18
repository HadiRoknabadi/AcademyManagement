using AcademyManagement.Domain.Attributes;
using AcademyManagement.Domain.Entities.Account;

namespace AcademyManagement.Domain.Entities.Term
{
    [Auditable]
    public class TermUser
    {
        #region Properties
        public int Id { get; set; }
        public int TermId { get; set; }
        public string UserId { get; set; }

        #endregion

        #region Relations

        public Term Term { get; set; }

        #endregion
    }
}
