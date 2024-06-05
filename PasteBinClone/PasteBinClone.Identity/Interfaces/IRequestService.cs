using PasteBinClone.Identity.Models;

namespace PasteBinClone.Identity.Interfaces
{
    public interface IRequestService
    {
        Task SendUser(ApiUser user);
    }
}
