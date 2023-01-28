using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.Services.Interfaces;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using WebSite.Endpoint.Pages.Common;

namespace WebSite.Endpoint.Pages.Account
{
    public class LoginModel : BasePage
    {

        #region  Constructor

        private readonly ICaptchaValidator _captchaValidator;
        private readonly IUserService _userService;

        public LoginModel(ICaptchaValidator captchaValidator, IUserService userService)
        {
            _captchaValidator = captchaValidator;
            _userService = userService;
        }



        #endregion

        public LoginDTO LoginDTO { get; set; } = new LoginDTO { };

        public void OnGet(string returnUrl)
        {

        }

        public async Task<IActionResult> OnPost(string returnUrl)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(LoginDTO.Captcha))
            {
                TempData[Toast_WarningMessage] = "کد کپچای شما تایید نشد";
                return Page();
            }

            if (ModelState.IsValid)
            {
                var res = await _userService.LoginUser(LoginDTO);

                switch (res)
                {
                    case LoginResult.NotFound:
                        TempData[SweetAlert_ErrorMessage] = "کاربری با این مشخصات یافت نشد";
                        break;

                    case LoginResult.PhoneNumberNotActivated:
                        TempData[SweetAlert_WarningMessage] = "حساب کاربری شما فعال نشده است . لطفا نسبت به فعالسازی آن دقت نمایید";
                        break;

                    case LoginResult.UserAccountIsBlocked:
                        TempData[SweetAlert_ErrorMessage] = "حساب کاربری شما مسدود شده است";
                        break;

                    case LoginResult.UnknownError:
                        TempData[SweetAlert_ErrorMessage] = "خطای ناشناخته ای رخ داد";
                        break;

                    case LoginResult.Success:
                        TempData[Toast_SuccessMessage] = "عملیات ورود با موفقیت انجام شد";
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
                        return Redirect("/");
                }
            }

            return Page();
        }
    }
}

