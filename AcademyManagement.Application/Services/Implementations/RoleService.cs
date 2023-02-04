using AcademyManagement.Application.DTOs.Role;
using AcademyManagement.Application.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Application.Services.Implementations
{
    public class RoleService:IRoleService
    {

        #region  Constructor

        private readonly RoleManager<IdentityRole> _roleManager;
          private readonly IMapper _mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }



        #endregion

        #region  Get All Roles

        public async Task<List<IdentityRole>> GetAllRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        #endregion

        #region  Add Role

        public async Task<AddRoleResult> AddRole(AddOrEditRoleDTO addRoleDTO)
        {
            if(await IsExistRoleByName(addRoleDTO.Name)) return AddRoleResult.RoleNameIsDuplcate;

            var role=_mapper.Map<AddOrEditRoleDTO,IdentityRole>(addRoleDTO);

            var res=await _roleManager.CreateAsync(role);

            return AddRoleResult.Success;
        }


        #endregion

        #region Edit Role

        public async Task<EditRoleResult> EditRole(AddOrEditRoleDTO editRoleDTO)
        {
            var role=await _roleManager.FindByIdAsync(editRoleDTO.Id);

            if(role==null) return EditRoleResult.NotFound;

            if(editRoleDTO.Name!=role.Name && await IsExistRoleByName(editRoleDTO.Name)) return EditRoleResult.RoleNameIsDuplicate;

            var editedRole=_mapper.Map<AddOrEditRoleDTO,IdentityRole>(editRoleDTO,role);

            var res=await _roleManager.UpdateAsync(editedRole);

            return EditRoleResult.Success;
        }


        #endregion

        #region Is Exist Role By Name

        public async Task<bool> IsExistRoleByName(string roleName)
        {
            return await _roleManager.Roles.AnyAsync(r=>r.Name==roleName);
        }


        #endregion
    }
}