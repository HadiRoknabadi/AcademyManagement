using AcademyManagement.Application.DTOs.Role;
using Microsoft.AspNetCore.Identity;

namespace AcademyManagement.Application.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<IdentityRole>> GetAllRoles();
        Task<AddRoleResult> AddRole(AddOrEditRoleDTO addRoleDTO);
        Task<EditRoleResult> EditRole(AddOrEditRoleDTO editRoleDTO);
        Task<bool> IsExistRoleByName(string roleName);
    }
}