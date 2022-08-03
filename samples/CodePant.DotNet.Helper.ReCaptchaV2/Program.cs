using System;
using System.Threading.Tasks;
using CodePant.DotNet.Helper.ReCaptchaV2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace CodePant.DotNet.Helper.ReCaptchaV2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine(typeof(Program));
            IHost? host = CreateHostBuilder(args).Build();

            string reCaptchaClientToken = "YOUR_CLIENT_TOKEN";
            Console.WriteLine(reCaptchaClientToken);

            var (isSuccess, reCaptchaResponse, response) = await host.Services.GetService<IReCaptchaV2>()?.VerifyRecaptcha(reCaptchaClientToken, false)!;
            Console.WriteLine($"skipViaSecret : false | {nameof(isSuccess)} : {isSuccess} | {nameof(response)} : {JsonConvert.SerializeObject(response)} | {nameof(reCaptchaResponse)} : {JsonConvert.SerializeObject(reCaptchaResponse)}");

            var (isSuccess2, reCaptchaResponse2, response2) = await host.Services.GetService<IReCaptchaV2>()?.VerifyRecaptcha(reCaptchaClientToken, true)!;
            Console.WriteLine($"skipViaSecret : true| {nameof(isSuccess2)} : {isSuccess2} | {nameof(response2)} : {JsonConvert.SerializeObject(response2)} | {nameof(reCaptchaResponse2)} : {JsonConvert.SerializeObject(reCaptchaResponse2)}");

            Console.WriteLine("Press any key to exit");
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddReCaptchaV2Extension(secret: hostContext.Configuration.GetValue<string>("ReCaptchaV2:Secret"));
                });
        }
    }
}