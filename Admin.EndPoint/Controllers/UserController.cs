using AcademyManagement.Application.DTOs.User;
using AcademyManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Admin.EndPoint.Controllers
{
    public class UserController:BaseController
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
            filter.HowManyShowPageAfterAndBefore=7;
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
        public IActionResult AddUser(AddUserDTO addUserDTO)
        {
            if(ModelState.IsValid)
            {

            }
            return View(addUserDTO);
        }

        #endregion

        
    }
}