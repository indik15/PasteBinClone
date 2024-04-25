using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Request;

namespace PasteBinClone.Web.Services
{
    public class BaseService : RequestService,
        IBaseService
    {
        private IHttpClientFactory _httpClient;

        public BaseService(IHttpClientFactory httpClient) 
            : base(httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> Delete<T>(int id, string route)
        {
            return await Send<T>(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.DELETE,
                Url = Settings.WebApiBase + route + id
            });
        }

        public async Task<T> GetAll<T>(string route)
        {
            return await Send<T>(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.GET,
                Url = Settings.WebApiBase + route
            });
        }

        public async Task<T> GetById<T>(int id, string route)
        {
            return await Send<T>(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.GET,
                Url = Settings.WebApiBase + route + id
            });
        }

        public async Task<T> Post<T>(object categoryVM, string route)
        {
            return await Send<T>(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.POST,
                Data = categoryVM,
                Url = Settings.WebApiBase + route
            });
        }

        public async Task<T> Put<T>(object categoryVM, string route)
        {
            return await Send<T>(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.PUT,
                Data = categoryVM,
                Url = Settings.WebApiBase + route
            });
        }
    }
}
