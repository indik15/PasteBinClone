using PasteBinClone.Application.ViewModels;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IApiUserService
    {
        Task<bool> CreateUser(ApiUser user);
        Task<ApiUser> GetApiUserById(string id);
    }
}
