using AcademyManagement.Domain.Attributes;
using Microsoft.AspNetCore.Identity;

namespace AcademyManagement.Domain.Entities.Account
{
    [Auditable]
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string FullName { get { return $"{Name} {Family}"; } }
    }
}
