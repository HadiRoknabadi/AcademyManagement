using Microsoft.Extensions.Configuration;

namespace AcademyManagement.Infrastructure.Senders
{
    public interface ISenderService
    {
        #region SendEmail

        void SendEmail(string  to,string subject,string body);

        #endregion

        #region Send SMS
        
        Task SendSMS(string mobile,string message);

        Task SendVerificationSMS(string mobile,string activationCode);

        Task SendForgotPasswordVerificationSMS(string mobile,string activationCode);

        Task SendUserPasswordSMS(string mobile,string password);

        Task SendSuccessfullPayment(string mobile,string userName,string orderId);

        string GetAPIKey(IConfiguration configuration);

        #endregion
    }
}
