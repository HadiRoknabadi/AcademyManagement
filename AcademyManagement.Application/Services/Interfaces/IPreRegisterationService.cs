using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.DTOs.PreRegisteration;
using AcademyManagement.Domain.Entities.Account;

namespace AcademyManagement.Application.Services.Interfaces
{
    public interface IPreRegisterationService
    {
        #region CRUD

        Task<AddPreRegisterationResult> AddPreRegisteration(PreRegisterationDTO preRegisterationDTO);
        Task<PreRegisteration> GetPreRegisterationById(int preRegisterationById);
        Task<EditPreRegisterationResult> EditPreRegisteration(PreRegisterationDTO preRegisterationDTO);
        Task<DeletePreRegisterationResult> DeletePreRegisteration(int preRegisterationId);


        #endregion

        Task<bool> IsExistPreRegisterationByPhoneNumber(string phoneNumber);

        Task<FilterPreRegisterationDTO> FilterPreRegisteration(FilterPreRegisterationDTO filter);

        Task<PreRegisteratinDetailsDTO> GetPreRegisteratinDetails(int preRegisterationId);
    }
}
