using PasteBinClone.Web.Request;

namespace PasteBinClone.Web.Interfaces
{
    public interface IRequestService
    {
        Task<TViewModel> Send<TViewModel>(ApiRequest apiRequest);
    }
}
