using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace AcademyManagement.Application.DTOs.Common
{
    public class CaptchaDTO
    {
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        public string Captcha { get; set; }
    }

    public class CaptchaDTOValidator:AbstractValidator<CaptchaDTO>
    {
        public CaptchaDTOValidator()
        {
            RuleFor(c => c.Captcha).NotNull().WithMessage("لطفا CAptcha را وارد کنید");
        }
    }
}
