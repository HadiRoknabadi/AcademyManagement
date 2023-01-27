using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Application.Services.Interfaces.Contexts;
using AcademyManagement.Domain.Entities.Account;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AcademyManagement.Application.Services.Implementations
{
    public class PreRegisterationService:IPreRegisterationService
    {
        #region Constructor

        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;

        private readonly IUserService _userService;

        public PreRegisterationService(IDatabaseContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }






        #endregion


        #region CRUD

        public async Task<AddPreRegisterationResult> AddPreRegisteration(PreRegisterationDTO preRegisterationDTO)
        {
            if(await _userService.IsExistUserByPhoneNumber(preRegisterationDTO.PhoneNumber) || await IsExistPreRegisterationByPhoneNumber(preRegisterationDTO.PhoneNumber)) return AddPreRegisterationResult.ExistUser;

            var preRegisteration=_mapper.Map<PreRegisterationDTO,PreRegisteration>(preRegisterationDTO);

            await _context.PreRegisterations.AddAsync(preRegisteration);

            await _context.SaveChangesAsync();

            return AddPreRegisterationResult.Success;
        }
        public async Task<PreRegisteration> GetPreRegisterationById(int preRegisterationId)
        {
            return await _context.PreRegisterations.FindAsync(preRegisterationId);
        }
        public async Task<EditPreRegisterationResult> EditPreRegisteration(PreRegisterationDTO preRegisterationDTO)
        {
            var preRegisteration = await GetPreRegisterationById(preRegisterationDTO.Id);

            if(preRegisteration==null) return EditPreRegisterationResult.NotFound;

            var editedPreRegisteration=_mapper.Map<PreRegisterationDTO,PreRegisteration>(preRegisterationDTO,preRegisteration);

            await _context.SaveChangesAsync();

            return EditPreRegisterationResult.Success;
        }
        public async Task<DeletePreRegisterationResult> DeletePreRegisteration(int preRegisterationId)
        {
            var preRegisteration=await GetPreRegisterationById(preRegisterationId);

            if(preRegisteration==null) return DeletePreRegisterationResult.NotFound;

            _context.PreRegisterations.Remove(preRegisteration);
            await _context.SaveChangesAsync();

            return DeletePreRegisterationResult.Success;
        }


        #endregion

        public async Task<bool> IsExistPreRegisterationByPhoneNumber(string phoneNumber)
        {
            return await _context.PreRegisterations.AnyAsync(p=>p.PhoneNumber==phoneNumber);
        }

    }
}
