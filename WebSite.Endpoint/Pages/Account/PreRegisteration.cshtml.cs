using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Endpoint.Pages.Account
{

    public class PreRegisterationModel : PageModel
    {

        #region Constructor

        private readonly IPreRegisterationService _preRegisterationService;

        public PreRegisterationModel(IPreRegisterationService preRegisterationService)
        {
            _preRegisterationService = preRegisterationService;
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
            if(ModelState.IsValid)
            {
                var res=await _preRegisterationService.AddPreRegisteration(PreRegisterationDTO);

                
            }

            var errors=ModelState.Values.SelectMany(e=>e.Errors);

            return Page();
        }
    }
}
