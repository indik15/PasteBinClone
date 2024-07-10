using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Services
{
    public class CommentService(IMapper mapper, ICommentRepository commentRepository) : ICommentService
    {
        private readonly IMapper _mapper = mapper;
        private readonly ICommentRepository _commentRepository = commentRepository;

        public async Task<bool> CreateComment(CommentDto commentDto)
        {
            Comment comment = _mapper.Map<Comment>(commentDto);

            //if the creation was successful, the method will return true
            bool result = await _commentRepository.Create(comment);

            if (result)
            {
                Log.Information("User: {@i}, added comment to paste:", comment.UserId, comment.PasteId);
                return true;
            }

            Log.Error("Error creating comment.");
            return false;
        }

        public async Task<bool> DeleteComment(Guid id)
        {
            //if the creation was successful, the method will return true
            bool result = await _commentRepository.Delete(id);

            if (result)
            {
                Log.Information("Comment {@i} successfully deleted.", id);
                return true;
            }

            Log.Error("Comment deletion error.");
            return false;
        }

        public async Task<(IEnumerable<CommentDto> comments, int totalPages)> GetAllComments(Guid pasteId, int page)
        {
            (IEnumerable<Comment> comments, int totalComments) = await _commentRepository.Get(pasteId, page);

            int totalPages = (int)Math.Ceiling((double)totalComments / Constants.CommentCount);

            if (comments == null)
            {
                Log.Information("Comment not found.");

                return (null, 0);
            }

            Log.Information("Received comments: {@Count}", comments.Count());

            IEnumerable<CommentDto> commentDtos = _mapper.Map<IEnumerable<CommentDto>>(comments);

            return (commentDtos, totalPages);
        }

        public async Task<CommentDto> GetCommentByID(Guid id)
        {
            Comment comment = await _commentRepository.GetById(id);

            if(comment == null)
            {
                Log.Information("Comment {@i} not found.", id);
                return null;
            }

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<bool> UpdateComment(CommentDto commentDto)
        {
            Comment comment = _mapper.Map<Comment>(commentDto);

            bool result = await _commentRepository.Update(comment);

            if (result)
            {
                Log.Information("Comment {@i} updated.", comment.Id);
                return true;
            }
            else
            {
                Log.Error("Comment update error.");
                return false;
            }
        }
    }
}
