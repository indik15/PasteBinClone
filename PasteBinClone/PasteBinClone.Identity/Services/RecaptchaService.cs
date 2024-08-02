using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PasteBinClone.Identity.Interfaces;
using PasteBinClone.Identity.Models;
using System.Net;

namespace PasteBinClone.Identity.Services
{
    public class RecaptchaService(IOptions<RecaptchaOptions> options) : IRecaptchaService
    {
        private readonly RecaptchaOptions _options = options.Value;

        public RecaptchaResponse ValidateCaptcha(string response)
        {
            using var client = new WebClient();

            string secret = _options.SecretKey;
            string url = $"{_options.Url}?secret={secret}&response={response}";

            var result = client.DownloadString(url);

            try
            {
                var recaptchaResult = JsonConvert.DeserializeObject<RecaptchaResponse>(result.ToString());

                return recaptchaResult;
            }
            catch (Exception)
            {
                return default(RecaptchaResponse);
            }
        }
    }
}
