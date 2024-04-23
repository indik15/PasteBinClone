using AutoMapper;
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
    public class ContentTypeService : IContentTypeService
    {
        private readonly IBaseRepository<ContentType> _repository;
        private readonly IMapper _mapper;

        public ContentTypeService(IBaseRepository<ContentType> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateContentType(ContentTypeDto contentTypeDto)
        {
            ContentType contentType = _mapper.Map<ContentType>(contentTypeDto);
            await _repository.Create(contentType);

            Log.Information("Object {@i} created successfully.", contentTypeDto.TypeName);
        }

        public async Task<bool> DeleteContentType(int id)
        {
            //if the deletion was successful, the method will return the object id
            int? result = await _repository.Delete(id);

            if(result == 0 || result == null)
            {
                Log.Error("Object deletion error.");

                return false;
            }
            else
            {
                Log.Information("Object {@i} successfully deleted.", result);

                return true;
            }
        }

        public async Task<IEnumerable<ContentTypeDto>> GetAllContentType()
        {
            IEnumerable<ContentType> contentTypes = await _repository.Get();

            if(contentTypes == null)
            {
                Log.Information("Object not found.");

                return null;
            }
            Log.Information("Received objects: {@Count}", contentTypes.Count());

            return _mapper.Map<IEnumerable<ContentTypeDto>>(contentTypes);
        }

        public async Task<ContentTypeDto> GetContentTypeById(int id)
        {
            ContentType contentType = await _repository.GetById(id);

            if(contentType == null)
            {
                Log.Information("Object {@i} not found.", id);

                return null;
            }

            return _mapper.Map<ContentTypeDto>(contentType);
        }

        public async Task<bool> UpdateContentType(ContentTypeDto contentTypeDto)
        {
            ContentType contentType = _mapper.Map<ContentType>(contentTypeDto);

            //if the update was successful, the method will return the object id
            int? result = await _repository.Update(contentType);

            if(result == 0 || result == null)
            {
                Log.Error("Object update error.");

                return false;
            }
            else
            {
                Log.Information("Object {@i} updated.", result);

                return true;
            }
        }
    }
}
