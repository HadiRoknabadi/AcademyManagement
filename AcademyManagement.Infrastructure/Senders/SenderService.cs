using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace AcademyManagement.Infrastructure.Senders
{
    public class SenderService : ISenderService
    {
        #region Constructor

        private readonly IConfiguration _configuration;

        public SenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        #endregion

        #region SendEmail

        public void SendEmail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("dev.hadiroknabadi@gmail.com", "عنوان");
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("dev.hadiroknabadi@gmail.com", _configuration["EmailPassword"]);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        #endregion

        #region Send SMS

        public string GetAPIKey(IConfiguration configuration)
        {
            return  configuration["SmsAPIKey"];
        }




        public async Task SendSMS(string mobile,string message)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(_configuration["SmsAPIKey"]);

            await api.Send(_configuration["SmsSenderNumber"],mobile,message);
        }


        public async Task SendVerificationSMS(string mobile, string activationCode)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(GetAPIKey(_configuration));
            await api.VerifyLookup(mobile, activationCode, _configuration["VerificationSMSPatternName"]);
        }

        public async Task SendForgotPasswordVerificationSMS(string mobile,string activationCode)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(GetAPIKey(_configuration));
            await api.VerifyLookup(mobile, activationCode, _configuration["ForgotPasswordVerificationSMSPatternName"]);
        }

        public async Task SendUserPasswordSMS(string mobile,string password)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(GetAPIKey(_configuration));
            await api.VerifyLookup(mobile,password, _configuration["UserPasswordSMSPatternName"]);
        }
        public async Task SendSuccessfullPayment(string mobile,string userName,string orderId)
        {
            Kavenegar.KavenegarApi api = new Kavenegar.KavenegarApi(GetAPIKey(_configuration));
            await api.VerifyLookup(mobile,userName,orderId,null, _configuration["SuccessfullPaymentPatternName"]);
        }

        #endregion

    }
}
