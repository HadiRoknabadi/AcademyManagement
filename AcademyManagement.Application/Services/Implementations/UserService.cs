using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.DTOs.Paging;
using AcademyManagement.Application.DTOs.User;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Application.DTOs.Common;
using AcademyManagement.Domain.Entities.Account;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AcademyManagement.Application.Generators;

namespace AcademyManagement.Application.Services.Implementations
{
    public class UserService:IUserService
    {
        #region Constructor

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUploader _uploader;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IUploader uploader, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _uploader = uploader;
            _roleManager = roleManager;
        }





        #endregion

        public async Task<bool> IsExistUserByPhoneNumber(string phoneNumber)
        {
            return await _userManager.Users.AsQueryable().AnyAsync(u=>u.PhoneNumber==phoneNumber);
        }

        public async Task<bool> IsExistUserByEmail(string email)
        {
            return await _userManager.Users.AsQueryable().AnyAsync(u=>u.Email==email);

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

        #region Add User

        public async Task<AddUserResult> AddUser(AddUserDTO addUserDTO)
        {
            if(await IsExistUserByPhoneNumber(addUserDTO.PhoneNumber)) return AddUserResult.PhoneNumberIsExist;

            if(string.IsNullOrEmpty(addUserDTO.Email)==false && await IsExistUserByEmail(addUserDTO.Email)) return AddUserResult.EmailIsExist;

            var user=_mapper.Map<AddUserDTO,User>(addUserDTO);

            user.InsertTime = DateTime.Now;
            user.UpdateTime = DateTime.Now;
           

            if(addUserDTO.AvatarFile!=null)
            {
                var imageName=Generator.GenerateUniqCode() + Path.GetExtension(addUserDTO.AvatarFile.FileName);

                var res=await _uploader.UploadImage(UploadFileType.Image,addUserDTO.AvatarFile,imageName,43,43);

                switch(res)
                {
                    case DTOs.Common.UploadResult.Success:
                        user.Avatar = imageName;
                        break;

                    case DTOs.Common.UploadResult.CantUploadFile:
                        return AddUserResult.CantUploadAvatar;
                }

            }
            else
            {
                user.Avatar="Default.jpg";
            }

            //Add Role To User

            await _userManager.AddToRoleAsync(user,addUserDTO.Role);

            var createUserResult=await _userManager.CreateAsync(user,addUserDTO.Password);


          

            return AddUserResult.Success;
        }


        #endregion


        #region  Get User Details For Edit

        public async Task<EditUserDTO> GetUserDetailsForEdit(string userId)
        {
            var user=await _userManager.FindByIdAsync(userId);

            var userDetails=_mapper.Map<User,EditUserDTO>(user);

            var userRole=await _userManager.GetRolesAsync(user);

            userDetails.Role=userRole.FirstOrDefault();

            return userDetails;
        }


        #endregion

        #region Edit User

        public async Task<EditUserResult> EditUser(EditUserDTO editUserDTO)
        {
            var user=await _userManager.FindByIdAsync(editUserDTO.Id);

            if(user==null) return EditUserResult.NotFoundUser;

            if(editUserDTO.PhoneNumber!=user.PhoneNumber && await IsExistUserByPhoneNumber(editUserDTO.PhoneNumber)) return EditUserResult.PhoneNumberIsExist;

            if(string.IsNullOrEmpty(editUserDTO.Email)==false && editUserDTO.Email!=user.Email && await IsExistUserByEmail(editUserDTO.Email)) return EditUserResult.EmailIsExist;

            var editedUser=_mapper.Map<EditUserDTO,User>(editUserDTO,user);

            editedUser.UpdateTime = DateTime.Now;

            if (editUserDTO.AvatarFile!=null)
            {
                var imageName=Generator.GenerateUniqCode() + Path.GetExtension(editUserDTO.AvatarFile.FileName);

                var res=await _uploader.UploadImage(UploadFileType.Image,editUserDTO.AvatarFile,imageName,43,43,user.Avatar);

                switch(res)
                {
                    case DTOs.Common.UploadResult.Success:
                        editedUser.Avatar = imageName;
                        break;

                    case DTOs.Common.UploadResult.CantUploadFile:
                        return EditUserResult.CantUploadAvatar;
                }

            }



             //Update User Roles

            var isInReceivedRole=await _userManager.IsInRoleAsync(user,editUserDTO.Role);

            if(isInReceivedRole==false)
            {
                var userRole=await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRoleAsync(editedUser,userRole.FirstOrDefault());

                await _userManager.AddToRoleAsync(editedUser,editUserDTO.Role);
            }

            var updateUserResult=await _userManager.UpdateAsync(editedUser);
          

            return EditUserResult.Success;

        }


        #endregion

        #region  Delete User 

        public async Task<DeleteUserResult> DeleteUser(string userId)
        {
            var user=await _userManager.FindByIdAsync(userId);

            if(user==null) return DeleteUserResult.NotFound;

            user.IsRemoved = true;
            user.RemovedTime= DateTime.Now;
            user.UpdateTime = DateTime.Now;

            await _userManager.UpdateAsync(user);

            return DeleteUserResult.Success;;
        }


        #endregion

    }
}
