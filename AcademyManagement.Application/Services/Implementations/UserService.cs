using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Domain.Entities.Account;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace AcademyManagement.Application.Services.Implementations
{
    public class UserService:IUserService
    {
        #region Constructor

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }






        #endregion

        public async Task<bool> IsExistUserByPhoneNumber(string phoneNumber)
        {
            var user= await _userManager.FindByNameAsync(phoneNumber);

            if (user!=null) return true;

            return false;
        }

        public async Task<LoginResult> LoginUser(LoginDTO loginDTO)
        {
            var user=await GetUserByPhoneNumber(loginDTO.PhoneNumber);

            if(user==null) return LoginResult.NotFound;

            if(user.PhoneNumberConfirmed==false) return LoginResult.PhoneNumberNotActivated;

            if(user.AccessFailedCount>=5) return LoginResult.UserAccountIsBlocked;

            await _signInManager.SignOutAsync();

           var res= await _signInManager.PasswordSignInAsync(user,loginDTO.Password,loginDTO.RememberMe,true);

           if(!res.Succeeded) return LoginResult.UnknownError;

           return LoginResult.Success;
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            return await _userManager.Users.AsQueryable().SingleOrDefaultAsync(u=>u.PhoneNumber==phoneNumber);
        }



    }
}
