using Microsoft.AspNetCore.Mvc;

namespace Admin.EndPoint.ViewComponents
{
  
        #region Admin Header

        public class AdminHeaderViewComponent : ViewComponent
        {
            public async Task<IViewComponentResult> InvokeAsync()
            {
                #region Constructor



                #endregion

                return View("AdminHeader");
            }
        }




        #endregion
    
}
