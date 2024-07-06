using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PasteBinClone.API.Response;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.ViewModels;
using Serilog;

namespace PasteBinClone.API.Controllers
{
    [ApiController]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseAPI>> Get(Guid id)
        {
            CommentDto result = await _commentService.GetCommentByID(id);

            if(result == null)
            {
                return NotFound();
            }
            else
            {
                CommentVM commentVM = _mapper.Map<CommentVM>(result);
                _responseAPI.Data = commentVM;

                return Ok(_responseAPI);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseAPI>> Get()
        {
            IEnumerable<CommentDto> commentDtos = await _commentService.GetAllComments();

            if(commentDtos == null)
            {
                return NotFound();
            }
            else
            {
                var commentVMs = _mapper.Map<IEnumerable<CommentVM>>(commentDtos);

                _responseAPI.Data = commentVMs;
                return Ok(_responseAPI);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAPI>> Put([FromBody] CommentDto commentDto)
        {
            var valid = _validator.Validate(commentDto);

            if (!valid.IsValid)
            {
                Log.Error("Validation Error: {i}", valid.Errors);

                return ValidationProblem();
            }

            bool result = await _commentService.UpdateComment(commentDto);

            if (result)
            {
                return Ok(_responseAPI);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseAPI>> Delete(Guid id)
        {
            bool result = await _commentService.DeleteComment(id);

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
