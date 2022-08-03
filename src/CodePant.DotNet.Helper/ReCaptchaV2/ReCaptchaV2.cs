using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CodePant.DotNet.Helper.ReCaptchaV2
{
    public class ReCaptchaV2 : IReCaptchaV2
    {
        private readonly ReCaptchaV2Configuration _reCaptchaV2Configuration;
        private readonly ILogger<ReCaptchaV2> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public ReCaptchaV2(IOptions<ReCaptchaV2Configuration> reCaptchaV2Configuration, ILogger<ReCaptchaV2> logger, IHttpClientFactory clientFactory)
        {
            _reCaptchaV2Configuration = reCaptchaV2Configuration.Value;
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<(bool isSuccess, ReCaptchaV2Response reCaptchaV2Response, HttpResponseMessage? response)> VerifyRecaptcha(string clientToken, bool skipViaSecret = false)
        {
            try
            {
                if (skipViaSecret && clientToken.Equals(_reCaptchaV2Configuration.Secret)) return (true, null, null);

                string url = "https://www.google.com/recaptcha/api/siteverify";

                var dictionary = new Dictionary<string, string>
                {
                    { "secret", _reCaptchaV2Configuration.Secret },
                    { "response", clientToken }
                };

                var postContent = new FormUrlEncodedContent(dictionary);

                var response = await _clientFactory.CreateClient().PostAsync(url, postContent);

                if (response.IsSuccessStatusCode)
                {
                    var reCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaV2Response>(await response.Content.ReadAsStringAsync());
                    if (reCaptchaResponse != null && reCaptchaResponse.Success)
                    {
                        return (true, reCaptchaResponse, response);
                    }
                    else
                    {
                        return (false, null, response);
                    }
                }
                else
                {
                    return (false, null, response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in {nameof(VerifyRecaptcha)}");
                throw;
            }
        }

    }
}
