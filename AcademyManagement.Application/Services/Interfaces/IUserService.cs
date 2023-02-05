using AcademyManagement.Application.DTOs.Account;
using AcademyManagement.Application.DTOs.User;
using AcademyManagement.Domain.Entities.Account;

namespace AcademyManagement.Application.Services.Interfaces
{
    public interface IUserService
    {

        Task<LoginResult> LoginUser(LoginDTO loginDTO);

        Task<bool> IsExistUserByPhoneNumber(string phoneNumber);

        Task<User> GetUserByPhoneNumber(string phoneNumber);

        Task<FilterUsersDTO> FilterUsers(FilterUsersDTO filter);

        Task<AddUserResult> AddUser(AddUserDTO addUserDTO);
        Task<EditUserResult> EditUser(EditUserDTO editUserDTO);
        Task<EditUserDTO> GetUserDetailsForEdit(string userId);

        Task<bool> IsExistUserByEmail(string email);
    }
}
