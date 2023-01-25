using AcademyManagement.Domain.Attributes;

namespace AcademyManagement.Domain.Entities
{
    [Auditable]
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
    }
}
