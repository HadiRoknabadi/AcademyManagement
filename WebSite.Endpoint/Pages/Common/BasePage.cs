using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Endpoint.Pages.Common
{
    public class BasePage:PageModel
    {
        protected string Toast_ErrorMessage = "Toast_ErrorMessage";
        protected string Toast_SuccessMessage = "Toast_SuccessMessage";
        protected string Toast_InfoMessage = "Toast_InfoMessage";
        protected string Toast_WarningMessage = "Toast_WarningMessage";

        protected string SweetAlert_ErrorMessage = "SweetAlert_ErrorMessage";
        protected string SweetAlert_SuccessMessage = "SweetAlert_SuccessMessage";
        protected string SweetAlert_InfoMessage = "SweetAlert_InfoMessage";
        protected string SweetAlert_WarningMessage = "SweetAlert_WarningMessage";
    }
}