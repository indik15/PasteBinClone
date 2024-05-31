namespace PasteBinClone.Web.Interfaces
{
    public interface IBaseService : IRequestService
    {
        Task<T> GetAll<T>(string route, string token);
        Task<T> GetById<T>(int id, string route, string token);
        Task<T> Post<T>(object obj, string route, string token);
        Task<T> Put<T>(object obj, string route, string token);
        Task<T> Delete<T>(int id, string route, string token);
    }
}
