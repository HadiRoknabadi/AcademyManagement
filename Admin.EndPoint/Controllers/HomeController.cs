using Microsoft.AspNetCore.Mvc;

namespace Admin.EndPoint.Controllers
{
    public class HomeController : BaseController
    {
        [Route("Admin/Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
