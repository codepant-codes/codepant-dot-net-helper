using System.Net.Http;
using System.Threading.Tasks;

namespace CodePant.DotNet.Helper.ReCaptchaV2
{
    public interface IReCaptchaV2
    {
        public Task<(bool isSuccess, ReCaptchaV2Response reCaptchaV2Response, HttpResponseMessage? response)> VerifyRecaptcha(string clientToken, bool skipViaSecret = false);
    }
}
