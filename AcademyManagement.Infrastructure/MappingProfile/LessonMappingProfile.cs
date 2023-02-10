using AcademyManagement.Application.DTOs.Lesson;
using AcademyManagement.Domain.Entities.Lesson;
using AutoMapper;

namespace AcademyManagement.Infrastructure.MappingProfile
{
    public class LessonMappingProfile:Profile
    {

        public LessonMappingProfile()
        {
            CreateMap<AddOrEditLessonDTO,Lesson>().ForMember(r=>r.Id,r=>r.Condition(a=>a.Id!=null));

            CreateMap<AddOrEditLessonDTO,Lesson>().ForMember(l=>l.Id,l=>l.Ignore()).ForMember(l=>l.Lesson_File,l=>l.Condition(a=>a.PdfFile!=null));
        }
    }
}