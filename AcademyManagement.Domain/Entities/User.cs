using AcademyManagement.Domain.Attributes;
using Microsoft.AspNetCore.Identity;

namespace AcademyManagement.Domain.Entities
{
    [Auditable]
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string Family { get; set; }
    }
}
