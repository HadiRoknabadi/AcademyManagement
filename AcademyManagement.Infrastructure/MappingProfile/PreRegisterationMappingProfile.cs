using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Domain.Entities.Account;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManagement.Infrastructure.MappingProfile
{
    public class PreRegisterationMappingProfile:Profile
    {
        public PreRegisterationMappingProfile()
        {
            CreateMap<PreRegisterationDTO, PreRegisteration>();
        }
    }
}
