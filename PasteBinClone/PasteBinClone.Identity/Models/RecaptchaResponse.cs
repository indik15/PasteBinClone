using Newtonsoft.Json;

namespace PasteBinClone.Identity.Models
{
    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }
        [JsonProperty("error-codes")]
        public IEnumerable<string> Errors { get; set; }
    }
}
