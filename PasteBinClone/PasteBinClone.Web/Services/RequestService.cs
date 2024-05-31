using Humanizer;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Request;
using System.Net.Http.Headers;
using System.Text;

namespace PasteBinClone.Web.Services
{
    public class RequestService : IRequestService
    {
        private IHttpClientFactory _httpClient { get; set; }

        public RequestService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TViewModel> Send<TViewModel>(ApiRequest apiRequest)
        {
            try
            {
                var client = _httpClient.CreateClient("PasteBinCloneAPI");

                HttpRequestMessage message = new HttpRequestMessage();

                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();

                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert
                        .SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                }

                switch (apiRequest.ApiMethod)
                {
                    case Settings.ApiMethod.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case Settings.ApiMethod.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Settings.ApiMethod.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content
                    .ReadAsStringAsync();

                var apiResponseVM = JsonConvert
                    .DeserializeObject<TViewModel>(apiContent);

                return apiResponseVM;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while executing the request.", ex);
            }
        }
    }
}