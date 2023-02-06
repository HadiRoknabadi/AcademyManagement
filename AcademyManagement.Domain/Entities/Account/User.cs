using AcademyManagement.Domain.Attributes;
using Microsoft.AspNetCore.Identity;

namespace AcademyManagement.Domain.Entities.Account
{
    [Auditable]
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Avatar { get; set; }
        public string FullName { get { return $"{Name} {Family}"; } }
        public DateTime InsertTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime RemovedTime { get; set; }
        public bool IsRemoved { get; set; }
    }
}
