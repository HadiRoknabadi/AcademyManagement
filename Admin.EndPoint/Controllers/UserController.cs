using AcademyManagement.Application.DTOs.User;
using AcademyManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Admin.EndPoint.Controllers
{
    public class UserController : BaseController
    {
        #region  Constructor

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }





        #endregion

        #region Users

        [Route("Admin/Users")]
        public async Task<IActionResult> Users(FilterUsersDTO filter)
        {
            filter.HowManyShowPageAfterAndBefore = 7;
            var users = await _userService.FilterUsers(filter);
            return View(users);
        }

        #endregion

        #region  Add User

        [Route("Admin/AddUser")]
        public async Task<IActionResult> AddUser()
        {

            #region Fill Role List

            ViewData["Roles"] = await _roleService.GetAllRoles();

            #endregion

            return View();
        }

        [Route("Admin/AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDTO addUserDTO)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.AddUser(addUserDTO);

                switch (res)
                {
                    case AddUserResult.CantUploadAvatar:
                        TempData[Toast_ErrorMessage] = "تصویر انتخاب شده قابل بارگذاری نمی باشد";
                        break;

                    case AddUserResult.PhoneNumberIsExist:
                        TempData[Toast_ErrorMessage] = "شماره موبایل وارد شده قبلا ثبت شده است";
                        break;

                    case AddUserResult.EmailIsExist:
                        TempData[Toast_ErrorMessage] = " ایمیل وارد شده قبلا ثبت شده است";
                        break;

                    case AddUserResult.Success:
                        TempData[Toast_SuccessMessage] = "کاربر با موفقیت ثبت شد";
                        return RedirectToAction(nameof(Users));
                }
            }

            #region Fill Role List

            ViewData["Roles"] = await _roleService.GetAllRoles();

            #endregion


            return View(addUserDTO);
        }

        #endregion


        #region  Edit User

        [Route("Admin/EditUser/{userId}")]
        public async Task<IActionResult> EditUser(string userId)
        {
            var userDetails=await _userService.GetUserDetailsForEdit(userId);

            if(userDetails==null)
            {
                TempData[SweetAlert_ErrorMessage]="کاربری با این مشخصصات یافت نشد";
                return RedirectToAction(nameof(Users));
            }

            #region Fill Role List

            ViewData["Roles"] = await _roleService.GetAllRoles();

            #endregion

            return View(userDetails);
        }

        [Route("Admin/EditUser/{userId?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserDTO editUserDTO)
        {
            if(ModelState.IsValid)
            {
                var res=await _userService.EditUser(editUserDTO);

                switch(res)
                {
                    case EditUserResult.Success:
                        TempData[SweetAlert_SuccessMessage] = "ویرایش کاربر با موفقیت انجام شد";
                        return RedirectToAction(nameof(Users));

                    case EditUserResult.NotFoundUser:
                        TempData[Toast_WarningMessage] = "کاربری با این مشخصات یافت نشد";
                        break;

                    case EditUserResult.PhoneNumberIsExist:
                        TempData[Toast_WarningMessage] = "شماره موبایل وارد شده قبلا در سایت ثبت شده است";
                        break;

                    case EditUserResult.EmailIsExist:
                        TempData[Toast_WarningMessage] = "ایمیل وارد شده قبلا در سایت ثبت شده است";
                        break;

                    case EditUserResult.CantUploadAvatar:
                        TempData[Toast_ErrorMessage] = "تصویر انتخاب شده قابل بارگذاری نمی باشد";
                        break;
                }

                
            }

            #region Fill Role List

            ViewData["Roles"] = await _roleService.GetAllRoles();

            #endregion

            return View(editUserDTO);
        }

        #endregion


    }
}