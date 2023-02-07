using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.DTOs.PreRegisteration;
using AcademyManagement.Domain.Entities.Account;
using AutoMapper;

namespace AcademyManagement.Infrastructure.MappingProfile
{
    public class PreRegisterationMappingProfile:Profile
    {
        public PreRegisterationMappingProfile()
        {
            CreateMap<PreRegisterationDTO, PreRegisteration>();
            CreateMap<PreRegisteration,PreRegisteratinDetailsDTO>();
        }
    }
}
