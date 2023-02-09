using System.ComponentModel.DataAnnotations;
using AcademyManagement.Application.DTOs.Paging;

namespace AcademyManagement.Application.DTOs.Lesson
{
    public class FilterLessonDTO:BasePaging
    {
         #region Properties
        public string LessonName { get; set; }
        public int SeasonCount { get; set; }
        public FilterLessonOrder OrderBy { get; set; }
        public List<AcademyManagement.Domain.Entities.Lesson.Lesson> Lessons { get; set; }

        #endregion

         #region Methods

        public FilterLessonDTO SetLessons(List<AcademyManagement.Domain.Entities.Lesson.Lesson> lessons)
        {
            this.Lessons = lessons;
            return this;
        }

        public FilterLessonDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;

            return this;
        }



        #endregion
    }

       public enum FilterLessonOrder
    {
        [Display(Name = "صعودی")]
        CreateDate_ASC,

        [Display(Name = "نزولی")]
        CreateDate_DES
    }
}