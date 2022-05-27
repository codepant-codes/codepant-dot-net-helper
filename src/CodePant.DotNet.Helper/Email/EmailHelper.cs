using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using CodePant.DotNet.Helper.Email.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CodePant.DotNet.Helper.Email
{
    public class EmailHelper : IEmailHelper
    {
        private readonly ILogger<EmailHelper> _logger;
        private SmtpOptions _smtpOptions;

        public EmailHelper(SmtpOptions options, ILogger<EmailHelper> logger)
        {
            this._logger = logger;
            this._smtpOptions = options;
            //this._smtpOptions = options.CurrentValue;
            //options.OnChange<SmtpOptions>(
            //    (cfg) =>
            //    {
            //        this._smtpOptions = cfg;
            //    }
            //);
        }

        /// <summary>
        /// Sends mailmessage using the provided smtp client
        /// </summary>
        /// <param name="smtpClient"></param>
        /// <param name="mailMessage"></param>
        public void SendEmail(SmtpClient smtpClient, MailMessage mailMessage)
        {
            using (smtpClient)
            {
                smtpClient.Send(mailMessage);
                this._logger.LogInformation("Email Sent Successfully");
            }
        }

        /// <summary>
        /// Sends mailmessage using the default client generated from smtpOptions
        /// </summary>
        /// <param name="mailMessage"></param>
        public void SendEmail(MailMessage mailMessage)
        {
            using (SmtpClient client = new())
            {
                #region Setting Up Client
                client.Host = _smtpOptions.Host;
                client.Port = _smtpOptions.Port;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpOptions.Username, _smtpOptions.Password);
                #endregion

                this.SendEmail(client, mailMessage);
            }
        }

        /// <summary>
        /// Send email with list of To MailAddress
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        public void SendEmail(MailAddress fromAddress, List<MailAddress> toAddress, string subject, string body, List<string> attachments)
        {
            #region Mail Message
            MailMessage mailMessage = new();

            // From 
            mailMessage.From = fromAddress;

            // To
            if (toAddress != null && toAddress.Count > 0)
            {
                for (int i = 0; i < toAddress.Count; i++)
                {
                    mailMessage.To.Add(toAddress[i]);
                }
            }


            // Subject
            mailMessage.Subject = subject;

            // Body
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            // Attachments
            if (attachments != null && attachments.Count > 0)
            {
                for (int i = 0; i < attachments.Count; i++)
                {
                    mailMessage.Attachments.Add(new Attachment(attachments[i]));
                }
            }
            #endregion

            this.SendEmail(mailMessage);
        }

        /// <summary>
        /// Sends email with list of To string address
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        public void SendEmail(string fromAddress, List<string> toAddress, string subject, string body, List<string> attachments)
        {
            List<MailAddress> maiLAddressList = new List<MailAddress>();

            for (int i = 0; i < toAddress.Count; i++)
            {
                maiLAddressList.Add(new MailAddress(address: toAddress[i]));
            }

            this.SendEmail(new MailAddress(fromAddress), maiLAddressList, subject, body, attachments);
        }

        /// <summary>
        /// Sends email with multiple attachments to single receiver
        /// </summary>
        /// <param name="fromAddress"></param>
        /// <param name="toAddress"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachments"></param>
        public void SendEmail(string fromAddress, string toAddress, string subject, string body, List<string> attachments)
        {
            List<MailAddress> maiLAddressList = new List<MailAddress>();
            maiLAddressList.Add(new MailAddress(address: toAddress));

            this.SendEmail(new MailAddress(fromAddress), maiLAddressList, subject, body, attachments);
        }
    }
}
