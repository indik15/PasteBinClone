using Duende.IdentityServer.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PasteBinClone.Identity.Interfaces;
using PasteBinClone.Identity.Models;
using PasteBinClone.Identity.Models.ViewModel;
using System.Security.Cryptography;
using System.Text;

namespace PasteBinClone.Identity.Services
{
    public class RequestService(IHttpClientFactory httpClient,
        IConfiguration configuration) : IRequestService
    {
        private IHttpClientFactory _httpClient = httpClient;
        private readonly IConfiguration _configuration = configuration;

        //Class to send the user to the API
        public async Task SendUser(ApiUser user)
        {
            if(user == null)
            {
                return;
            }

            var client = _httpClient.CreateClient("PasteBinCloneAPI");

            string apiSecret = CreateHash(_configuration["SendUserSettings:SecretKey"]);
            string apiUrl = _configuration["SendUserSettings:Url"];

            using HttpRequestMessage request = new HttpRequestMessage();

            request.Headers.Add("Accept", "application/json");
            request.RequestUri = new Uri(apiUrl);
            client.DefaultRequestHeaders.Clear();

            request.Content = new StringContent(JsonConvert
                .SerializeObject(new ApiUserViewModel
                {
                    ApiUser = user,
                    ApiCode = apiSecret
                }), Encoding.UTF8, "application/json");

            request.Method = HttpMethod.Post;

            var res = await client.SendAsync(request);

            var apiContent = await res.Content
                    .ReadAsStringAsync();
        }

        private string CreateHash(string key)
        {
            using SHA256 sHA256 = SHA256.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(key);

            var hash = sHA256.ComputeHash(bytes);

            var result = new StringBuilder();

            foreach (var item in hash)
            {
                result.Append(item.ToString("x2"));
            }
            return result.ToString();
        }
    }
}
