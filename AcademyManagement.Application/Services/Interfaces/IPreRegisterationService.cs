using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
