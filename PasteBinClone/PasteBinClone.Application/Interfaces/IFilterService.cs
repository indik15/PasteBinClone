using PasteBinClone.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IFilterService
    {
        Task<FilterVM> GetAllFilters();
    }
}
