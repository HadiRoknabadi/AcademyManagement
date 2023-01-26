using FluentValidation;

namespace AcademyManagement.Application.DTOs.Account
{
    public class PreRegisterationDTO
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string PhoneNumber { get; set; }
        public string Grade { get; set; }
        public bool WentToEnglishClassBefore { get; set; }
        public int? HowManyTermDidYouPass { get; set; }
        public string BookNameReadInEnglishClass { get; set; }
        public string Description { get; set; }
    }

    public class PreRegisterationDTOValidator:AbstractValidator<PreRegisterationDTO>
    {
        public PreRegisterationDTOValidator()
        {
            RuleFor(p => p.Name).NotNull().Length(1, 150).WithMessage("لطفا نام خود را وارد کنید");

            RuleFor(p => p.Family).NotNull().Length(1, 150).WithMessage("لطفا نام خانوادگی خود را وارد کنید");

            RuleFor(p => p.PhoneNumber).NotNull().Length(1, 50).WithMessage("لطفا شماره موبایل خود را وارد کنید");

            RuleFor(p => p.Grade).NotNull().Length(1, 300).WithMessage("لطفا مقطع تحصیلی خود را وارد کنید");

            RuleFor(p => p.HowManyTermDidYouPass).InclusiveBetween(1,int.MaxValue).WithMessage("لطفا مقطع تحصیلی خود را وارد کنید");

            RuleFor(p => p.BookNameReadInEnglishClass).Length(1, 300).WithMessage("لطفا مقطع تحصیلی خود را وارد کنید");

            RuleFor(p => p.BookNameReadInEnglishClass).Length(1, 300).NotNull().WithMessage("لطفا مقطع تحصیلی خود را وارد کنید");
        }
    }
}
