using FluentValidation;
using Microsoft.AspNetCore.Http;

public class AddUserDTO
{
        public string Name { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public bool IsEmailActive { get; set; }
        public bool IsPhoneNumberActive { get; set; }
        public string Password { get; set; }
        public IFormFile AvatarFile { get; set; }
}

public class AddUserDTOValidator:AbstractValidator<AddUserDTO>
{
    public AddUserDTOValidator()
    {
            RuleFor(p => p.Name).Length(1, 200).WithMessage("نام نمی تواند کمتر از 1 و بیشتر از 200 کاراکتر باشد").NotNull().WithMessage("لطفا نام را وارد کنید");
            RuleFor(p => p.Family).Length(1, 200).WithMessage("نام خانوادگی نمی تواند کمتر از 1 و بیشتر از 200 کاراکتر باشد").NotNull().WithMessage("لطفا نام خانوادگی را وارد کنید");
            RuleFor(p => p.Email).Length(1, 200).WithMessage("ایمیل نمی تواند کمتر از 1 و بیشتر از 200 کاراکتر باشد").EmailAddress().WithMessage("ایمیل وارد شده معتبرنمی باشد").NotNull().WithMessage("لطفا ایمیل را وارد کنید");
            RuleFor(p => p.PhoneNumber).Length(11, 11).WithMessage("شماره موبایل نمی تواند کمتر از 11 و بیشتر از 11 کاراکتر باشد").Matches(@"^09(0[1-9]|1[0-9]|2[0-9]|3[0-9]|9[0-9]).{7}$").WithMessage("شماره موبایل وارد شده معتبرنمی باشد").NotNull().WithMessage("لطفا شماره موبایل را وارد کنید");
            RuleFor(p => p.RoleId).NotNull().WithMessage("لطفا نقش کاربر را مشخص کنید");
            RuleFor(p => p.Password).Length(8, 25).WithMessage(" رمز عبور نمی تواند کمتر از 8 و بیشتر از 25 کاراکتر باشد").NotNull().WithMessage("لطفا رمز عبور را وارد کنید");       
    }
}