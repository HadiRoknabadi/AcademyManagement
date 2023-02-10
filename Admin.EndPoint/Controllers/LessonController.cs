using AcademyManagement.Application.DTOs.Lesson;
using AcademyManagement.Application.Services.Interfaces;
using AcademyManagement.Infrastructure.Http;
using Microsoft.AspNetCore.Mvc;

namespace Admin.EndPoint.Controllers
{
    public class LessonController : BaseController
    {
        #region Constructor

        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        #endregion

        #region Lessons

        [Route("Admin/Lessons")]
        public async Task<IActionResult> Lessons(FilterLessonDTO filter)
        {
            var lessons=await _lessonService.FilterLessons(filter);
            return View(lessons);
        }

        #endregion

        #region  Add Lesson

        [Route("Admin/AddLesson")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLesson(AddOrEditLessonDTO addLessonDTO)
        {
            if (ModelState.IsValid)
            {
                var res = await _lessonService.AddLesson(addLessonDTO);

                switch (res)
                {
                    case AddLessonResult.ExistLesson:
                        return JsonResponseStatus.SendStatus(JsonResponseStatusType.Error, "نام درس وارد شده تکراری است", null);

                     case AddLessonResult.CantUploadFile:
                        return JsonResponseStatus.SendStatus(JsonResponseStatusType.Error, "فایل انتخاب شده قابل بارگذاری نمی باشد", null);

                    case AddLessonResult.Success:
                        return JsonResponseStatus.SendStatus(JsonResponseStatusType.Success, "درس با موفقیت ثبت شد", null);


                }
            }
            return View(addLessonDTO);
        }

        #endregion
    }
}