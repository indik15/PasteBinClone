namespace PasteBinClone.Web.Interfaces
{
    public interface IBaseService : IRequestService
    {
        Task<T> GetAll<T>(string route);
        Task<T> GetById<T>(int id, string route);
        Task<T> Post<T>(object obj, string route);
        Task<T> Put<T>(object obj, string route);
        Task<T> Delete<T>(int id, string route);
    }
}
