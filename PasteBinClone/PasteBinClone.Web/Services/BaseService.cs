using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;

namespace PasteBinClone.Web.Services
{
    public class BaseService(IRequestService requestService) : IBaseService
    {
        private readonly IRequestService _requestService = requestService;

        public async Task<ResponseAPI> Delete(object id, string route, string token)
        {
            return await _requestService.Send(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.DELETE,
                Url = Settings.WebApiBase + route + id,
                AccessToken = token
            });
        }

        public async Task<ResponseAPI> GetAll(string route, string token, object data = null)
        {
            return await _requestService.Send(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.GET,
                Url = Settings.WebApiBase + route,
                AccessToken = token,
                Data = data
            });
        }

        public async Task<ResponseAPI> GetById(object id, string route, string token, object obj = null, string userId = null, string password = null)
        {
            var url = $"{Settings.WebApiBase}{route}{id}";

            var queryParameters = new List<string>();

            if (!string.IsNullOrEmpty(userId))
            {
                queryParameters.Add($"userId={userId}");
            }
            if (!string.IsNullOrEmpty(password))
            {
                queryParameters.Add($"password={password}");
            }

            if (queryParameters.Any())
            {
                url += "?" + string.Join("&", queryParameters);
            }

            return await _requestService.Send(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.GET,
                Url = url,
                AccessToken = token,
                Data = obj,
            });
        }


        public async Task<ResponseAPI> Post(object objectVM, string route, string token)
        {
            return await _requestService.Send(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.POST,
                Data = objectVM,
                Url = Settings.WebApiBase + route,
                AccessToken = token
            });
        }

        public async Task<ResponseAPI> Put(object objectVM, string route, string token)
        {
            return await _requestService.Send(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.PUT,
                Data = objectVM,
                Url = Settings.WebApiBase + route,
                AccessToken = token
            });
        }
    }
}
