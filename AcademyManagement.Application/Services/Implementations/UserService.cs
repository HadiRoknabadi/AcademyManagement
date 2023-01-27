using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Application.Services.Interfaces.Contexts;
using AcademyManagement.Domain.Entities.Account;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyManagement.Application.Services.Implementations
{
    public class UserService:IUserService
    {
        #region Constructor

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }




        #endregion

        public async Task<bool> IsExistUserByPhoneNumber(string phoneNumber)
        {
            var user= await _userManager.FindByNameAsync(phoneNumber);

            if (user!=null) return true;

            return false;
        }

    }
}
