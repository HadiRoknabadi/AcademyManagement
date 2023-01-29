using AcademyManagement.Application.DTOs.Common;
using FluentValidation;

namespace AcademyManagement.Application.DTOs.Account
{
    public class PreRegisterationDTO:CaptchaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Grade { get; set; }
        public bool WentToEnglishClassBefore { get; set; }
        public int? HowManyTermDidYouPass { get; set; }
        public string BookNameReadInEnglishClass { get; set; }
        public string Description { get; set; }
    }

    public enum AddPreRegisterationResult
    {
        Success,
        ExistUser
    }

    public enum EditPreRegisterationResult
    {
        Success,
        NotFound
    }

    public enum DeletePreRegisterationResult
    {
        Success,
        NotFound
    }


    public class PreRegisterationDTOValidator:AbstractValidator<PreRegisterationDTO>
    {
        public PreRegisterationDTOValidator()
        {
            RuleFor(p => p.Name).Length(1, 150).WithMessage("نام نمی تواند کمتر از 1 و بیشتر از 150 کاراکتر باشد").NotNull().WithMessage("لطفا نام خود را وارد کنید");

            RuleFor(p => p.Family).Length(1, 150).WithMessage("نام خانوادگی نمی تواند کمتر از 1 و بیشتر از 150 کاراکتر باشد").NotNull().WithMessage("لطفا نام خانوادگی خود را وارد کنید");

            RuleFor(p => p.PhoneNumber).Length(11, 11).WithMessage("نام خانوادگی نمی تواند کمتر یا بیشتر از 11 کاراکتر باشد").NotNull().WithMessage("لطفا شماره موبایل خود را وارد کنید");

            RuleFor(p => p.Grade).Length(1, 300).NotNull().WithMessage(" مقطع تحصیلی نمی تواند کمتر از 1 و بیشتر از 300 کاراکتر باشد").WithMessage("لطفا مقطع تحصیلی خود را وارد کنید");

            RuleFor(p => p.HowManyTermDidYouPass).InclusiveBetween(1,int.MaxValue).WithMessage("عدد وارد شده خارج از محدوده است");

            RuleFor(p => p.BookNameReadInEnglishClass).Length(1, 300).WithMessage("نام کتاب نمی تواند کمتر از 1 و بیشتر از 300 کاراکتر باشد");

            RuleFor(p => p.Description).Length(1, 300).WithMessage("متن توضیح دلخواه نمی تواند کمتر از 1 و بیشتر از 300 کاراکتر باشد");
        }
    }
}
