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
        Task<bool> GetById(string id);
        Task<bool> Create(ApiUser user);
        Task<bool> Update(ApiUser user);    
    }
}
