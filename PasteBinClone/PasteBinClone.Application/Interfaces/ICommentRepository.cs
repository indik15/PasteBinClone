using PasteBinClone.Application.Dto;
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
        Task<(IEnumerable<Comment> comments, int totalComments)> Get(Guid pasteId, int page);
        Task<bool> Create(Comment comment);
        Task<bool> Delete(Guid id);
    }
}
