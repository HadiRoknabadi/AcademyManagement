using AcademyManagement.Application.DTOs.Role;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Infrastructure.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.EndPoint.Controllers
{
    public class RoleController : BaseController
    {
        #region  Constructor

        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        #endregion

        #region  Roles

        [Route("Admin/Roles")]
        public async Task<IActionResult> Roles()
        {
            var roles = await _roleService.GetAllRoles();
            return View(roles);
        }

        #endregion

        #region  Add Role

        [Route("Admin/AddRole")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(AddOrEditRoleDTO addOrEditRoleDTO)
        {
            if (ModelState.IsValid)
            {
                var res = await _roleService.AddRole(addOrEditRoleDTO);

                switch (res)
                {
                    case AddRoleResult.RoleNameIsDuplcate:
                        return JsonResponseStatus.SendStatus(JsonResponseStatusType.Error, "نام نقش وارد شده تکراری است", null);

                    case AddRoleResult.Success:
                        return JsonResponseStatus.SendStatus(JsonResponseStatusType.Success, "نقش با موفقیت ثبت شد", null);


                }
            }
            return View(addOrEditRoleDTO);
        }

        #endregion

        #region  Edit Role

        [Route("Admin/EditRole")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(AddOrEditRoleDTO editRoleDTO)
        {
            if (ModelState.IsValid)
            {
                var res = await _roleService.EditRole(editRoleDTO);

                switch (res)
                {
                    case EditRoleResult.RoleNameIsDuplicate:
                        return JsonResponseStatus.SendStatus(JsonResponseStatusType.Error, "نام نقش وارد شده تکراری است", null);

                    case EditRoleResult.NotFound:
                        return JsonResponseStatus.SendStatus(JsonResponseStatusType.Warning, "نقشی با این مشخصات یافت نشد",  null);


                    case EditRoleResult.Success:
                        return JsonResponseStatus.SendStatus(JsonResponseStatusType.Success, "نقش با موفقیت ویرایش شد", null);


                }
            }
            return View(editRoleDTO);
        }

        #endregion
    }
}