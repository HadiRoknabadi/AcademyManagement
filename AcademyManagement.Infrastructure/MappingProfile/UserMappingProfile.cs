using AcademyManagement.Domain.Entities.Account;
using AutoMapper;

namespace AcademyManagement.Infrastructure.MappingProfile
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AddUserDTO, User>().ForMember(u=>u.UserName,u=>u.MapFrom(a=>a.PhoneNumber)).ForMember(u => u.PhoneNumberConfirmed, u => u.MapFrom(a => a.IsPhoneNumberActive)).ForMember(u => u.EmailConfirmed, u => u.MapFrom(a => a.IsEmailActive)).ForMember(u=>u.Email,u=>u.Condition(a=>a.Email!=null));
        }
    }
}
