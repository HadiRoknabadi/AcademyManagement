
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Endpoint.ViewComponents
{
    #region Site MetaTags

    public class SiteMetaTags:ViewComponent
    {
       public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteMetaTags");
        }
    }

    #endregion

    #region Site Header

    public class SiteHeader : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteHeader");
        }
    }

    #endregion

    #region Site Footer

    public class SiteFooter : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteFooter");
        }
    }

    #endregion
}
