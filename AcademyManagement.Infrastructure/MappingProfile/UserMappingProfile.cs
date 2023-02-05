using AcademyManagement.Domain.Entities.Account;
using AutoMapper;

namespace AcademyManagement.Infrastructure.MappingProfile
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AddUserDTO, User>().ForMember(u=>u.UserName,u=>u.MapFrom(a=>a.PhoneNumber)).ForMember(u => u.PhoneNumberConfirmed, u => u.MapFrom(a => a.IsPhoneNumberActive)).ForMember(u => u.EmailConfirmed, u => u.MapFrom(a => a.IsEmailActive)).ForMember(u=>u.Email,u=>u.Condition(a=>a.Email!=null));

            CreateMap<User,EditUserDTO>().ForMember(u=>u.Password,u=>u.Ignore()).ForMember(u=>u.IsEmailActive,u=>u.MapFrom(a=>a.EmailConfirmed)).ForMember(u=>u.IsPhoneNumberActive,u=>u.MapFrom(a=>a.PhoneNumberConfirmed)).ForMember(u=>u.Id,u=>u.MapFrom(a=>a.Id));

            CreateMap<EditUserDTO,User>().ForMember(p => p.Avatar, d => d.Ignore()).ForMember(u => u.PasswordHash, u => u.Condition(u => u.Password != null));
        }
    }
}
