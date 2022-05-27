using System;
using CodePant.DotNet.Helper.Email.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CodePant.DotNet.Helper.Email
{
    public static class EmailExtension
    {
        public static void AddEmailHelperExtension(this IServiceCollection services, SmtpOptions smtpOptions)
        {
            _ = services.AddScoped<IEmailHelper, EmailHelper>(
                provider => new EmailHelper(
                    smtpOptions, provider.GetService<ILogger<EmailHelper>>() ?? throw new ArgumentNullException(nameof(ILogger<EmailHelper>), "Parameter is null")
                    )
                );
        }
    }
}
