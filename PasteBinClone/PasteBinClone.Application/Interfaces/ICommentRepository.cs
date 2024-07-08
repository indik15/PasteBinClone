using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> Get(Guid pasteId);
        Task<Comment> GetById(Guid id);
        Task<bool> Create(Comment comment);
        Task<bool> Update(Comment comment);
        Task<bool> Delete(Guid id);
    }
}
