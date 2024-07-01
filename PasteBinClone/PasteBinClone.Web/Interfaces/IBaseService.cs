using PasteBinClone.Web.Models;

namespace PasteBinClone.Web.Interfaces
{
    public interface IBaseService
    {
        Task<ResponseAPI> GetAll(string route, string token = null);
        Task<ResponseAPI> GetById(object id, string route, string token, string userId = null, string password = null);
        Task<ResponseAPI> Post(object obj, string route, string token);
        Task<ResponseAPI> Put(object obj, string route, string token);
        Task<ResponseAPI> Delete(object id, string route, string token);
    }
}
