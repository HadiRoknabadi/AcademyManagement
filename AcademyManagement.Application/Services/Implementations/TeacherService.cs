using AcademyManagement.Application.DTOs.Term;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Application.Services.Interfaces.Contexts;
using AcademyManagement.Domain.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Application.Services.Implementations
{
    public class TeacherService : ITeacherService
    {
        #region Constructor

        private readonly IDatabaseContext _context;
        private readonly UserManager<User> _userManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public TeacherService(IDatabaseContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }





        #endregion

        #region Get Teachers For Term
        public async Task<List<TeachersForTermDTO>> GetTeachersForTerm()
        {
            var teachers = await _userManager.GetUsersInRoleAsync("مدرس");
            return teachers.Select(t=>new TeachersForTermDTO{
                Id=t.Id,
                FullName=t.FullName
            }).ToList();

        }
        #endregion

    }
}