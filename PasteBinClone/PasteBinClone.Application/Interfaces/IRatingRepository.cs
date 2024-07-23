using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IRatingRepository
    {
        Task<bool> Create(Rating rating);
        Task<bool> Update(Rating rating);
    }
}
