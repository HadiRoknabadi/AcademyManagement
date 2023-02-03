using AcademyManagement.Application.DTOs.User;
using AcademyManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Admin.EndPoint.Controllers
{
    public class UserController : BaseController
    {
        #region  Constructor

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
        public IActionResult AddUser()
        {
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
            return View(addUserDTO);
        }

        #endregion


    }
}