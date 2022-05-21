using System;
using CodePant.DotNet.Helper.Email.Options;
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

            host.Services.GetService<IEmailHelper>()?
                .SendEmail(fromAddress, toAddress, subject, body);

            Console.WriteLine("Press any key to exit");
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<SmtpOptions>(hostContext.Configuration.GetSection("SmtpConfiguration"));

                    services.AddScoped<IEmailHelper, EmailHelper>();
                });
        }
    }
}