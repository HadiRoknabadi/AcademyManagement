using FluentValidation;

namespace AcademyManagement.Application.DTOs.Role
{
    public class AddOrEditRoleDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }     
    }

    public class AddOrEditRoleDTOValidator:AbstractValidator<AddOrEditRoleDTO>
    {
        public AddOrEditRoleDTOValidator()
        {
            RuleFor(r=>r.Name).NotNull().WithMessage("نام نقش نمی تواند خالی باشد");
        }
    }

    public enum AddRoleResult
    {
        Success,
        RoleNameIsDuplcate
    }

    public enum EditRoleResult
    {
        Success,
        NotFound,
        RoleNameIsDuplicate
    }
}