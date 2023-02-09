using AcademyManagement.Application.DTOs.Lesson;
using AcademyManagement.Application.Services.Interfaces;
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
    }
}