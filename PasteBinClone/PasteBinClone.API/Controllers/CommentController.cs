using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.API.Response;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.ViewModels;
using Serilog;

namespace PasteBinClone.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/comments")]
    public class CommentController(IValidator<CommentDto> validator, 
        ICommentService commentService,
        IMapper mapper) : ControllerBase
    {
        private readonly IValidator<CommentDto> _validator = validator;
        private readonly ICommentService _commentService = commentService;
        private readonly IMapper _mapper = mapper;
        private readonly ResponseAPI _responseAPI = new();

        [HttpPost]
        public async Task<ActionResult<ResponseAPI>> Post([FromBody] CommentDto commentDto)
        {
            var valid = _validator.Validate(commentDto);

            if (!valid.IsValid)
            {
                Log.Error("Validation Error: {i}", valid.Errors);

                return ValidationProblem();
            }

            bool result = await _commentService.CreateComment(commentDto);

            if (result)
            {
                return Ok(_responseAPI);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{pasteId}")]
        public async Task<ActionResult<ResponseAPI>> GetAll([FromBody] int pageNumber, Guid pasteId)
         {
            (IEnumerable<CommentDto> commentDtos, int totalPages) = await _commentService.GetAllComments(pasteId, pageNumber);

            if(commentDtos == null)
            {
                return NotFound();
            }
            else
            {
                var commentVMs = _mapper.Map<IEnumerable<CommentVM>>(commentDtos);

                _responseAPI.Data = new CommentsResponse
                {
                    CommentVMs = commentVMs,
                    TotalPages = totalPages
                };

                return Ok(_responseAPI);
            }
        }

        [HttpDelete("{commentId}")]
        public async Task<ActionResult<ResponseAPI>> Delete(Guid commentId)
        {
            bool result = await _commentService.DeleteComment(commentId);

            if (result)
            {
                return Ok(_responseAPI);
            }
            else
            {
                return NotFound();
            }
        }


    }
}
