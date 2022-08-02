using Microsoft.Extensions.DependencyInjection;

namespace CodePant.DotNet.Helper.ReCaptchaV3
{
    public static class ReCaptchaV3Extension
    {
        public static void AddReCaptchaV3Extension(this IServiceCollection services, string secret)
        {
            _ = services.Configure<ReCaptchaV3Configuration>(
                    (options) =>
                    {
                        options.Secret = secret;
                    }
                );

            _ = services.AddScoped<IReCaptchaV3, ReCaptchaV3>();
        }
    }
}
