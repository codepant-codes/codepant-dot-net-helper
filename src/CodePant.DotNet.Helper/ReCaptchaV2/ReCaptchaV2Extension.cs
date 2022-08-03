using Microsoft.Extensions.DependencyInjection;

namespace CodePant.DotNet.Helper.ReCaptchaV2
{
    public static class ReCaptchaV2Extension
    {
        public static void AddReCaptchaV2Extension(this IServiceCollection services, string secret)
        {
            _ = services.Configure<ReCaptchaV2Configuration>(
                    (options) =>
                    {
                        options.Secret = secret;
                    }
                );
            services.AddHttpClient<IReCaptchaV2>();
            _ = services.AddSingleton<IReCaptchaV2, ReCaptchaV2>();
        }
    }
}
