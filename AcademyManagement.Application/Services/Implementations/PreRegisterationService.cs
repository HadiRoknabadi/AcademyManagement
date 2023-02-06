using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.DTOs.Paging;
using AcademyManagement.Application.DTOs.PreRegisteration;
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

        #region Filter PreRegisteration

        public async Task<FilterPreRegisterationDTO> FilterPreRegisteration(FilterPreRegisterationDTO filter)
        {
            var query=_context.PreRegisterations.AsQueryable().AsNoTracking();

            #region Order

            switch (filter.OrderBy)
            {
                case FilterPreRegisterationOrder.CreateDate_ASC:
                    query = query.OrderBy(u => EF.Property<DateTime>(u, "InsertTime"));
                    break;

                case FilterPreRegisterationOrder.CreateDate_DES:
                    query = query.OrderByDescending(u => EF.Property<DateTime>(u, "InsertTime"));
                    break;
            }

            #endregion

            #region Filter

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(u => EF.Functions.Like(u.Name, $"%{filter.Name}%"));

            if (!string.IsNullOrEmpty(filter.Family))
                query = query.Where(u => EF.Functions.Like(u.Family, $"%{filter.Family}%"));

            if (!string.IsNullOrEmpty(filter.PhoneNumber))
                query = query.Where(u => EF.Functions.Like(u.PhoneNumber, $"%{filter.PhoneNumber}%"));

            if (!string.IsNullOrEmpty(filter.Grade))
                query = query.Where(u => EF.Functions.Like(u.Grade, $"%{filter.Grade}%"));

            #endregion

            #region Paging

            var allEntitiesCount = await query.CountAsync();

            var pager = Pager.Build(filter.PageId, allEntitiesCount, filter.TakeEntity, filter.HowManyShowPageAfterAndBefore);

            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filter.SetPaging(pager).SetPreRegisterations(allEntities);
        }
        

        #endregion

    }
}
