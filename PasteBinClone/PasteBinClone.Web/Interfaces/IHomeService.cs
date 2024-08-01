using PasteBinClone.Web.Models.ViewModel;
using PasteBinClone.Web.Models.ViewModel.Paste;

namespace PasteBinClone.Web.Interfaces
{
    public interface IHomeService
    {
        Task<IEnumerable<HomePasteVM>> GetAllUserPastes(string accessToken);
        Task<IEnumerable<HomePasteVM>> GetTopRatedPastes();
        Task<FilterVM> GetAllFilters();

    }
}
