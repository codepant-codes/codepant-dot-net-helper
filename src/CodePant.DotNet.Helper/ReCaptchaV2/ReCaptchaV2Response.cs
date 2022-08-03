using System;
using Newtonsoft.Json;

namespace CodePant.DotNet.Helper.ReCaptchaV2
{
    public class ReCaptchaV2Response
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("challenge_ts")]
        public DateTime ChallengeTs { get; set; }

        [JsonProperty("hostname")]
        public string HostName { get; set; }

        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
