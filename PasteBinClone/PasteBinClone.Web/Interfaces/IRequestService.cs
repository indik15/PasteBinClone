using PasteBinClone.Web.Models;
using PasteBinClone.Web.Request;

namespace PasteBinClone.Web.Interfaces
{
    public interface IRequestService
    {
        Task<ResponseAPI> Send(ApiRequest apiRequest);
    }
}
