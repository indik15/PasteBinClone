using PasteBinClone.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface ICommentService
    {
        Task<CommentDto> GetCommentByID(Guid id);
        Task<IEnumerable<CommentDto>> GetAllComments();
        Task<bool> CreateComment(CommentDto commentDto);
        Task<bool> DeleteComment(Guid id);
        Task<bool> UpdateComment(CommentDto commentDto);
    }
}
