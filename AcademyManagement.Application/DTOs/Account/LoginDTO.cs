using AcademyManagement.Application.DTOs.Common;
using FluentValidation;

namespace AcademyManagement.Application.DTOs.Account
{
    public class LoginDTO:CaptchaDTO
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }

    public enum LoginResult
    {
        Success,
        NotFound,
        EmailNotActivated,
        PhoneNumberNotActivated,
        UserAccountIsBlocked,
        UnknownError
    }

    public class LoginDTOValidator:AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(u=>u.PhoneNumber).Length(11,11).WithMessage("شماره موبایل نمی تواند بیشتر یا کمتر از 11 کاراکتر باشد").Matches(@"^09(0[1-9]|1[0-9]|2[0-9]|3[0-9]|9[0-9]).{7}$").WithMessage("شماره موبایل وارد شده صحیح نمی باشد").NotNull().WithMessage("لطفا شماره موبایلی که با آن ثبت نام کردید را وارد کنید");

            RuleFor(u=>u.Password).Length(8,25).WithMessage("رمز عبور نمی تواند کمتر از 8 و بیشتر از 25 کاراکتر باشد").NotNull().WithMessage("لطفا رمز عبور خود را وارد کنید");
        }
    }
}
