using AcademyManagement.Application.DTOs.Role;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AcademyManagement.Infrastructure.MappingProfile
{
    public class RoleMappingProfile:Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<AddOrEditRoleDTO,IdentityRole>().ForMember(r=>r.Id,r=>r.Condition(a=>a.Id!=null));
        }
    }
}