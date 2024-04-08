namespace PasteBinClone.Web.Interfaces
{
    public interface IBaseService : IRequestService
    {
        Task<T> GetAll<T>();
        Task<T> GetById<T>(int id);
        Task<T> Post<T>(object obj);
        Task<T> Put<T>(object obj);
        Task<T> Delete<T>(int id);
    }
}
