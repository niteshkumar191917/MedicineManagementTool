using MailKit.Net.Smtp;
using MedicineManagementTool.BAL.IService;
using MedicineMAnagementTool.Common.DTOs;
using MedicineMAnagementTool.Common.Email_Send;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;

namespace MedicineManagementTool.BAL.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(UserDTO request)
        {
            EmailDTO emailDTO = new EmailDTO();
            emailDTO.To = request.Email;
            emailDTO.Subject = $"Welcome {request.Name}";
            string body = string.Empty;
            string resultPath = Environment.CurrentDirectory.Replace("MedicineManagementTool.API", "MedicineMAnagementTool.Common");
            using (StreamReader reader = new StreamReader(resultPath + @"\EmailTemplate\EmailIndex.html"))
            {
                body = reader.ReadToEnd();
            }
            
            body = body.Replace("{UserName}",request.Email);
            body = body.Replace("{Password}",request.Password);
            emailDTO.Body = body;
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(emailDTO.To));
            email.Subject = emailDTO.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = emailDTO.Body };

            string smtpMail = _config.GetSection("EmailHost").Value;
            int smtpPort = 465;
            string senderEmail = _config.GetSection("EmailUserName").Value;
            string senderPassword = _config.GetSection("EmailPassword").Value;

            EmailCommon.SendEmail(smtpMail, smtpPort, email, senderEmail, senderPassword);

        }
    }
}
