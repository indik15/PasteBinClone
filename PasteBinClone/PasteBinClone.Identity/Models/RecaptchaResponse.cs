using Newtonsoft.Json;

namespace PasteBinClone.Identity.Models
{
    public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }
    }
}
