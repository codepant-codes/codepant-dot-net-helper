using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using CodePant.DotNet.Helper.Email.Options;
using Microsoft.Extensions.Logging;

namespace CodePant.DotNet.Helper.Email
{
    public class EmailHelper : IEmailHelper
    {
        private readonly ILogger<EmailHelper> _logger;
        private SmtpOptions _smtpOptions;

        public EmailHelper(IOptionsMonitor<SmtpOptions> options, ILogger<EmailHelper> logger)
        {
            this._logger = logger;
            this._smtpOptions = options.CurrentValue;
            options.OnChange<SmtpOptions>(
                (cfg) =>
                {
                    this._smtpOptions = cfg;
                }
            );
        }

        public void SendEmail(string fromAddress, string toAddress, string subject, string body)
        {
            using (SmtpClient client = new())
            {
                client.Host = _smtpOptions.Host;
                client.Port = _smtpOptions.Port;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_smtpOptions.Username, _smtpOptions.Password);

                MailMessage mailMessage = new();
                mailMessage.From = new MailAddress(fromAddress);
                mailMessage.To.Add(new MailAddress(toAddress));
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                client.Send(mailMessage);
                this._logger.LogInformation("Email Sent Successfully");
            }
        }
    }
}
