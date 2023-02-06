using AcademyManagement.Application.DTOs.PreRegisteration;
using AcademyManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Admin.EndPoint.Controllers
{
    public class PreRegisterationController : BaseController
    {
        #region Constructor

        private readonly IPreRegisterationService _preRegisterationService;

        public PreRegisterationController(IPreRegisterationService preRegisterationService)
        {
            _preRegisterationService = preRegisterationService;
        }

        #endregion

        #region  PreRegisterations
        #endregion

        [Route("Admin/PreRegisterations")]
        public async Task<IActionResult> PreRegisterations(FilterPreRegisterationDTO filter)
        {
            filter.HowManyShowPageAfterAndBefore=7;
            var preRegisterations=await _preRegisterationService.FilterPreRegisteration(filter);

            return View(preRegisterations);
        }
    }
}