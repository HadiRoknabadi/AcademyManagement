using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Domain.Entities.Account;

namespace AcademyManagement.Application.Services.Interfaces
{
    public interface IUserService
    {

        Task<LoginResult> LoginUser(LoginDTO loginDTO);

        Task<bool> IsExistUserByPhoneNumber(string phoneNumber);

        Task<User> GetUserByPhoneNumber(string phoneNumber);
    }
}
