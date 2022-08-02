using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodePant.DotNet.Helper.ReCaptchaV3
{
    internal interface IReCaptchaV3
    {
        public Task<(bool isSuccess, List<string> errors, HttpResponseMessage? response)> VerifyRecaptcha(string clientToken, bool skipViaSecret = false);
    }
}
