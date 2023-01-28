using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebSite.Endpoint.Pages.Common
{
    public class BasePage:PageModel
    {
        protected string Toast_ErrorMessage = "ToastErrorMessage";
        protected string Toast_SuccessMessage = "ToastSuccessMessage";
        protected string Toast_InfoMessage = "ToastInfoMessage";
        protected string Toast_WarningMessage = "ToastWarningMessage";

        protected string SweetAlert_ErrorMessage = "SweetAlertErrorMessage";
        protected string SweetAlert_SuccessMessage = "SweetAlertSuccessMessage";
        protected string SweetAlert_InfoMessage = "SweetAlertInfoMessage";
        protected string SweetAlert_WarningMessage = "SweetAlertWarningMessage";
    }
}