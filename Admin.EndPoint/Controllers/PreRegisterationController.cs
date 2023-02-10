using AcademyManagement.Application.DTOs.PreRegisteration;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Infrastructure.Http;
using Microsoft.AspNetCore.Mvc;
using AcademyManagement.Application.DTOs.Account;

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

        [Route("Admin/PreRegisterations")]
        public async Task<IActionResult> PreRegisterations(FilterPreRegisterationDTO filter)
        {
            filter.HowManyShowPageAfterAndBefore = 7;
            var preRegisterations = await _preRegisterationService.FilterPreRegisteration(filter);

            return View(preRegisterations);
        }

        #endregion

        #region PreRegisteratin Details

        [Route("Admin/PreRegisteratinDetails/{preRegisterationId}")]
        public async Task<IActionResult> PreRegisteratinDetails(int preRegisterationId)
        {
            var preRegisteration = await _preRegisterationService.GetPreRegisteratinDetails(preRegisterationId);
            if (preRegisteration == null)
            {
                TempData[Toast_ErrorMessage] = "اطلاعاتی یافت نشد";
                return RedirectToAction(nameof(PreRegisterations));
            }
            return View(preRegisteration);

        }

        #endregion

        #region  Delete PreRegisteration

        [Route("Admin/DeletePreRegisteratin/{preRegisterationId}")]
        public async Task<IActionResult> DeletePreRegisteraton(int preRegisterationId)
        {
      
                var res = await _preRegisterationService.DeletePreRegisteration(preRegisterationId);

                switch (res)
                {
                    case DeletePreRegisterationResult.NotFound:
                        return JsonResponseStatus.SendStatus(JsonResponseStatusType.Warning, "پیش ثبت نامی با این مشخصات یافت نشد", null);


                    case DeletePreRegisterationResult.Success:
                        return JsonResponseStatus.SendStatus(JsonResponseStatusType.Success, "پیش ثبت نامی با موفقیت حذف شد", null);


                }
            
            return JsonResponseStatus.SendStatus(JsonResponseStatusType.Error, "عملیات مورد نظر با خطا مواجه شد", null);
        }


        #endregion

    }


}