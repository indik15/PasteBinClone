using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using PasteBinClone.Web.Interfaces;
using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Models.ViewModel.Paste;
using PasteBinClone.Web.Request;

namespace PasteBinClone.Web.Services
{
    public class HomeService(IBaseService baseService, 
        IDistributedCache cache,
        IUserInfo userInfo) : IHomeService
    {
        private readonly IBaseService _baseService = baseService;
        private readonly IDistributedCache _cache = cache;
        private readonly IUserInfo _userInfo = userInfo;

        public async Task<FilterVM> GetAllFilters()
        {
            string cacheName = "filters";

            string filtersCache = await _cache.GetStringAsync(cacheName);

            if (filtersCache != null)
            {
                return JsonConvert.DeserializeObject<FilterVM>(filtersCache);
            }
            else
            {
                var responseFilter = await _baseService.GetAll(RouteConst.FilterRoute);

                if(responseFilter != null && responseFilter.IsSuccess)
                {
                    FilterVM filters = JsonConvert.DeserializeObject<FilterVM>(responseFilter.Data.ToString());

                    string newCache = JsonConvert.SerializeObject(filters);

                    await _cache.SetStringAsync(cacheName, newCache, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                    });

                    return filters;
                }

                return null;
            }           
        }

        public async Task<IEnumerable<HomePasteVM>> GetAllUserPastes(string accessToken)
        {
            if (accessToken != null)
            {
                string userId = _userInfo.GetUserId(accessToken);

                string userIdCache = await _cache.GetStringAsync(userId);

                if (userIdCache != null)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<HomePasteVM>>(userIdCache);
                }
                else
                {
                    var userPasteResponse = await _baseService.GetById(userId, RouteConst.HomeRoute, accessToken);

                    if (userPasteResponse != null && userPasteResponse.IsSuccess)
                    {
                        IEnumerable<HomePasteVM> userPastes = JsonConvert.DeserializeObject<IEnumerable<HomePasteVM>>(userPasteResponse.Data.ToString());

                        string newUserCache = JsonConvert.SerializeObject(userPastes);

                        await _cache.SetStringAsync(userId, newUserCache, new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
                        });

                        return userPastes;
                    }

                    return null;
                }
            }
            return null;         
        }

        public async Task<IEnumerable<HomePasteVM>> GetTopRatedPastes()
        {
            string cacheName = "topPaste";

            string topPasteCache = await _cache.GetStringAsync(cacheName);

            if (topPasteCache != null)
            {
                return JsonConvert.DeserializeObject<IEnumerable<HomePasteVM>>(topPasteCache);
            }
            else
            {
                var topRatedPastesResponse = await _baseService.GetAll(RouteConst.HomeRoute);

                if (topRatedPastesResponse != null && topRatedPastesResponse.IsSuccess)
                {
                    IEnumerable<HomePasteVM> topRatedPastes = JsonConvert.DeserializeObject<IEnumerable<HomePasteVM>>(topRatedPastesResponse.Data.ToString());

                    string newCache = JsonConvert.SerializeObject(topRatedPastes);

                    await _cache.SetStringAsync(cacheName, newCache, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                    });

                    return topRatedPastes;
                }

                return null;
            }
        }
    }
}
