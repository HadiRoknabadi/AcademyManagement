using Microsoft.AspNetCore.Mvc;

namespace Admin.EndPoint.Controllers
{
    public class BaseController : Controller
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
