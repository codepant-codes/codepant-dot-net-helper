using System.Collections.Generic;
using System.Net.Mail;

namespace CodePant.DotNet.Helper.Email
{
    public interface IEmailHelper
    {
        void SendEmail(SmtpClient smtpClient, MailMessage mailMessage);
        void SendEmail(MailMessage mailMessage);
        void SendEmail(MailAddress fromAddress, List<MailAddress> toAddress, string subject, string body, List<string> attachments);
        void SendEmail(string fromAddress, List<string> toAddress, string subject, string body, List<string> attachments);
        void SendEmail(string fromAddress, string toAddress, string subject, string body, List<string> attachments);
    }
}