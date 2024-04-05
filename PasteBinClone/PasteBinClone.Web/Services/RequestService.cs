using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Request;
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

            HttpResponseMessage apiResponse = null;

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

            apiResponse = await client.SendAsync(message);

            var apiContent = await apiResponse.Content
                .ReadAsStringAsync();

            var apiResponseVM = JsonConvert
                .DeserializeObject<TViewModel>(apiContent);

            return apiResponseVM;
        }
    }
}