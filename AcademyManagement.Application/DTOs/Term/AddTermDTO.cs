using FluentValidation;

namespace AcademyManagement.Application.DTOs.Term
{
    public class AddTermDTO
    {
        public string TermName { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public List<TermLessonsAndTeachers> TermLessonsAndTeachers { get; set; }
        
    }

    public class TermLessonsAndTeachers
    {
        public int LessonId { get; set; }
        public int TeacherId { get; set; }
    }

    public class AddTermDTOValidator:AbstractValidator<AddTermDTO>
    {
        public AddTermDTOValidator()
        {
            RuleFor(t=>t.TermName).NotNull().WithMessage("نام ترم نمی تواند خالی باشد").Length(1,200).WithMessage("نام ترم نمی تواند کمتر از 1 و یا بیشتر از 200 کاراکتر باشد");

            RuleFor(t=>t.Start_Date).NotNull().WithMessage("تاریخ شروع ترم نمی تواند خالی باشد").Matches(@"^[1-4]\d{3}\/((0[1-6]\/((3[0-1])|([1-2][0-9])|(0[1-9])))|((1[0-2]|(0[7-9]))\/(30|31|([1-2][0-9])|(0[1-9]))))$").WithMessage("فرمت تاریخ صحیح نمی باشد");

            RuleFor(t=>t.End_Date).NotNull().WithMessage("تاریخ پایان ترم نمی تواند خالی باشد").Matches(@"^[1-4]\d{3}\/((0[1-6]\/((3[0-1])|([1-2][0-9])|(0[1-9])))|((1[0-2]|(0[7-9]))\/(30|31|([1-2][0-9])|(0[1-9]))))$").WithMessage("فرمت تاریخ صحیح نمی باشد");

        }
    }
}
