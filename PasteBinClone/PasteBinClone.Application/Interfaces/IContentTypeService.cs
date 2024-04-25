using PasteBinClone.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IContentTypeService
    {
        Task<ContentTypeDto> GetContentTypeById(int id);
        Task<IEnumerable<ContentTypeDto>> GetAllContentType();
        Task<bool> CreateContentType(ContentTypeDto contentTypeDto);
        Task<bool> DeleteContentType(int id);
        Task<bool> UpdateContentType(ContentTypeDto contentTypeDto);
    }
}
