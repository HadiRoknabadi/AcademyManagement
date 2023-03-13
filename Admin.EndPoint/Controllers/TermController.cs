using System.Net.Http.Headers;
using AcademyManagement.Application.DTOs.Term;
using AcademyManagement.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Admin.EndPoint.Controllers
{
    public class TermController:BaseController
    {
        #region Constructor

        private readonly ITermService _termService;

        public TermController(ITermService termService)
        {
            _termService = termService;
        }

        #endregion

        #region Terms

        [Route("Admin/Terms")]
        public async Task<IActionResult> Terms(FilterTermDTO filter)
        {
            var terms=await _termService.FilterTerms(filter);
            return View(terms);
        }

        #endregion

        #region Add Term

        [Route("Admin/AddTerm")]
        public IActionResult AddTerm()
        {
            return View();
        }

        [Route("Admin/AddTerm")]
        [HttpPost]
        public IActionResult AddTerm(AddTermDTO addTermDTO)
        {
            if (ModelState.IsValid)
            {

            }
            return View(addTermDTO);
        }

        #endregion
    }
}