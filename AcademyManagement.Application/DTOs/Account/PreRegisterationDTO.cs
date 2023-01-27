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
            RuleFor(p => p.Name).NotNull().Length(1, 150).WithMessage("لطفا نام خود را وارد کنید");

            RuleFor(p => p.Family).NotNull().Length(1, 150).WithMessage("لطفا نام خانوادگی خود را وارد کنید");

            RuleFor(p => p.PhoneNumber).NotNull().Length(1, 50).WithMessage("لطفا شماره موبایل خود را وارد کنید");

            RuleFor(p => p.Grade).NotNull().Length(1, 300).WithMessage("لطفا مقطع تحصیلی خود را وارد کنید");

            RuleFor(p => p.HowManyTermDidYouPass).InclusiveBetween(1,int.MaxValue).WithMessage("عدد وارد شده خارج از محدوده است");

            RuleFor(p => p.BookNameReadInEnglishClass).Length(1, 300).WithMessage("نام کتاب نمی تواند بیشتر از 300 کاراکتر باشد");

            RuleFor(p => p.Description).Length(1, 300).NotNull().WithMessage("متن توضیح دلخواه نمی تواند بیشتر از 300 کاراکتر باشد");
        }
    }
}
