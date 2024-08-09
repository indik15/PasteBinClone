using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IApiUserRepository
    {
        Task<ApiUser> GetById(string id);
        Task<ApiUser> GetByIdWithUserPasteInfo(string id);
        Task<bool> Create(ApiUser user);
    }
}
