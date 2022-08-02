using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CodePant.DotNet.Helper.ReCaptchaV3
{
    public class ReCaptchaV3 : IReCaptchaV3
    {
        private readonly ReCaptchaV3Configuration _reCaptchaV3Configuration;
        private readonly ILogger<ReCaptchaV3> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public ReCaptchaV3(IOptions<ReCaptchaV3Configuration> reCaptchaV3Configuration, ILogger<ReCaptchaV3> logger, IHttpClientFactory clientFactory)
        {
            _reCaptchaV3Configuration = reCaptchaV3Configuration.Value;
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<(bool isSuccess, List<string> errors, HttpResponseMessage? response)> VerifyRecaptcha(string clientToken, bool skipViaSecret = false)
        {
            try
            {
                if (skipViaSecret && clientToken.Equals(_reCaptchaV3Configuration.Secret)) return (true, new List<string>() { "skipped via secret" }, null);

                string url = "https://www.google.com/recaptcha/api/siteverify";

                var dictionary = new Dictionary<string, string>
                {
                    { "secret", _reCaptchaV3Configuration.Secret },
                    { "response", clientToken }
                };

                var postContent = new FormUrlEncodedContent(dictionary);

                var response = await _clientFactory.CreateClient().PostAsync(url, postContent);

                if (response.IsSuccessStatusCode)
                {
                    var reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaV3Response>(await response.Content.ReadAsStringAsync());
                    if (reCaptchaResponse != null && !reCaptchaResponse.Success)
                    {
                        return (false, reCaptchaResponse.ErrorCodes.ToList(), response);
                    }
                    else
                    {
                        return (true, new List<string>(), response);
                    }
                }
                else
                {
                    return (false, new List<string>() { $"Google Response {nameof(response.IsSuccessStatusCode)} is : {response.IsSuccessStatusCode}" }, response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in {nameof(VerifyRecaptcha)}");
                return (false, new List<string>() { e.ToString() }, null);
            }
        }

    }
}
