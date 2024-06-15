using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IAmazonStorageService
    {
        Task<(bool, string)> UploadFile(string body);
        Task<string> GetFile(string id);
        Task<bool> DeleteFile(string id);
        Task<bool> UpdateFile(string id, string body);
    }
}
