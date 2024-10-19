using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace MedicineMAnagementTool.Common.Email_Send
{
    public class EmailCommon
    {
        public static void SendEmail(string smtpMail,int smtpPort, object email, string senderEmail,string senderPassword)
        {
            var smtp = new SmtpClient();
            smtp.Connect(smtpMail, smtpPort);
            smtp.Authenticate(senderEmail, senderPassword);
            smtp.Send((MimeMessage)email);
            smtp.Disconnect(true);
        }
    }
}
