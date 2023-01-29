using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.DTOs.Paging;
using AcademyManagement.Application.DTOs.User;
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

        #region  Filter Users

        public async Task<FilterUsersDTO> FilterUsers(FilterUsersDTO filter)
        {
             var query = _userManager.Users.AsQueryable().AsNoTracking();

            #region State

            switch (filter.State)
            {
                case FilterUserState.All:
                    break;

                case FilterUserState.Blocked:
                    query = query.Where(u => u.AccessFailedCount>=5);
                    break;

                case FilterUserState.NotBlocked:
                    query = query.Where(u => u.AccessFailedCount<5);
                    break;

                case FilterUserState.Actived:
                    query = query.Where(u => u.PhoneNumberConfirmed);
                    break;

                case FilterUserState.NotActived:
                    query = query.Where(u => u.PhoneNumberConfirmed == false);
                    break;

            }

            #endregion

            #region Order

            // switch (filter.OrderBy)
            // {
            //     case FilterUserOrder.CreateDate_ASC:
            //         query = query.OrderBy(u => u.);
            //         break;

            //     case FilterUserOrder.CreateDate_DES:
            //         query = query.OrderByDescending(u => u.CreateDate);
            //         break;
            // }

            #endregion

            #region User Role

            // switch (filter.UserRole)
            // {
            //     case FilterUserRole.All:
            //         break;

            //     case FilterUserRole.Admin:
            //         query = query.Where(u => u.RoleId == 1);
            //         break;

            //     case FilterUserRole.NormalUser:
            //         query = query.Where(u => u.RoleId == 2);
            //         break;
            // }

            #endregion

            #region Filter

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(u => EF.Functions.Like(u.Name, $"%{filter.Name}%"));

            if (!string.IsNullOrEmpty(filter.Family))
                query = query.Where(u => EF.Functions.Like(u.Family, $"%{filter.Family}%"));

            if (!string.IsNullOrEmpty(filter.Email))
                query = query.Where(u => EF.Functions.Like(u.Email, $"%{filter.Email}%"));

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
                query = query.Where(u => EF.Functions.Like(u.PhoneNumber, $"%{filter.PhoneNumber}%"));

            #endregion

            #region Paging

            var allEntitiesCount = await query.CountAsync();

            var pager = Pager.Build(filter.PageId, allEntitiesCount, filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetPaging(pager).SetUsers(allEntities);
        }


        #endregion



    }
}
