using System;
using System.Collections.Generic;
using System.Linq;
using CodePant.DotNet.Helper.Email.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodePant.DotNet.Helper.Email
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(typeof(Program));
            var host = CreateHostBuilder(args).Build();

            string fromAddress = args[0];
            string toAddress = args[1];
            string subject = args[2];
            string body = args[3];
            string attachmentPath = args[4];

            for (int i = 0; i < args.ToList().Count; i++)
            {
                Console.WriteLine(args[i]);
            }

            host.Services.GetService<IEmailHelper>()?
                .SendEmail(fromAddress, toAddress, subject, body, new List<string> { attachmentPath });

            Console.WriteLine("Press any key to exit");
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddEmailHelperExtension(new SmtpOptions
                    {
                        Host = hostContext.Configuration.GetValue<string>("SmtpConfiguration:Host"),
                        Password = hostContext.Configuration.GetValue<string>("SmtpConfiguration:Password"),
                        Username = hostContext.Configuration.GetValue<string>("SmtpConfiguration:Username"),
                        Port = hostContext.Configuration.GetValue<int>("SmtpConfiguration:Port")
                    });
                });
        }
    }
}