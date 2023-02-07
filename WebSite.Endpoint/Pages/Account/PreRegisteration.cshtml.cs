using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.Services.Interfaces;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using WebSite.Endpoint.Pages.Common;

namespace WebSite.Endpoint.Pages.Account
{

    public class PreRegisterationModel : BasePage
    {

        #region Constructor

        private readonly IPreRegisterationService _preRegisterationService;
        private readonly ICaptchaValidator _captchaValidator;

        public PreRegisterationModel(IPreRegisterationService preRegisterationService, ICaptchaValidator captchaValidator)
        {
            _preRegisterationService = preRegisterationService;
            _captchaValidator = captchaValidator;
        }



        #endregion


        #region Model Properties

        [BindProperty]
        public PreRegisterationDTO PreRegisterationDTO { get; set; } = new PreRegisterationDTO { };

        #endregion

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(PreRegisterationDTO.Captcha))
            {
                TempData[Toast_WarningMessage]="کد کپچای شما تایید نشد";
                return Page();
            }

            if (ModelState.IsValid)
            {
                var res = await _preRegisterationService.AddPreRegisteration(PreRegisterationDTO);

                switch(res)
                {
                    case AddPreRegisterationResult.Success:
                        TempData[SweetAlert_SuccessMessage]="ثبت نام با موفقیت انجام شد . آموزشگاه به زودی با شما تماس میگیرد";
                        return RedirectToPage("Index");
                        
                        case AddPreRegisterationResult.ExistUser:
                        TempData[SweetAlert_ErrorMessage]="شما قبلا ثبت نام کردید و امکان ثبت نام مجدد وجود ندارد";
                        return Redirect("Index");

                }

            }
            return Page();
        }
    }
}
