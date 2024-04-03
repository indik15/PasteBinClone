using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<int> Create(T obj);
        Task<int?> Update(T obj);
        Task<int?> Delete(int id);
    }
}
