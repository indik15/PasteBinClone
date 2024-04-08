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

        public async Task<T> Delete<T>(int id)
        {
            return await Send<T>(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.DELETE,
                Url = Settings.WebApiBase + "api/filter/" + id
            });
        }

        public async Task<T> GetAll<T>()
        {
            return await Send<T>(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.GET,
                Url = Settings.WebApiBase + "api/filter"
            });
        }

        public async Task<T> GetById<T>(int id)
        {
            return await Send<T>(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.GET,
                Url = Settings.WebApiBase + "api/filter/" + id
            });
        }

        public async Task<T> Post<T>(object categoryVM)
        {
            return await Send<T>(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.POST,
                Data = categoryVM,
                Url = Settings.WebApiBase + "api/filter"
            });
        }

        public async Task<T> Put<T>(object categoryVM)
        {
            return await Send<T>(new ApiRequest()
            {
                ApiMethod = Settings.ApiMethod.PUT,
                Data = categoryVM,
                Url = Settings.WebApiBase + "api/filter"
            });
        }
    }
}
