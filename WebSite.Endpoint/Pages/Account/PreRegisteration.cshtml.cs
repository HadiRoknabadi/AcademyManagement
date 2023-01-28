using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.Services.Interfaces;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(PreRegisterationDTO.Captcha))
            {
                
                return Page();
            }

            if (ModelState.IsValid)
            {
                var res = await _preRegisterationService.AddPreRegisteration(PreRegisterationDTO);


            }

            var errors = ModelState.Values.SelectMany(e => e.Errors);

            return Page();
        }
    }
}
