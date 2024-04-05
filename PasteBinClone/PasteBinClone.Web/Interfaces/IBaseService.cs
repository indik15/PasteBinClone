namespace PasteBinClone.Web.Interfaces
{
    public interface IBaseService : IRequestService
    {
        Task<IEnumerable<T>> GetAll<T>();
        Task<T> GetById<T>(int id);
        Task Post<T>(T obj);
        Task Put<T>(T obj);
        Task Delete<T>(int id);
    }
}
