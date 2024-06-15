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
    public class PasteService(IPasteRepository pasteRepository, 
        IMapper mapper,
        IAmazonStorageService amazonStorage) : IPasteService
    {
        private readonly IPasteRepository _pasteRepository = pasteRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAmazonStorageService _amazonStorage = amazonStorage;

        public async Task<bool> CreatePaste(PasteDto pasteDto)
        {
            (bool isCreate, string pasteId ) = 
                await _amazonStorage.UploadFile(pasteDto.Body);

            if (isCreate && pasteId != null)
            {
                Paste paste = new()
                {
                    Title = pasteDto.Title,
                    BodyUrl = pasteId,
                    IsPublic = pasteDto.IsPublic,
                    CreateAt = DateTime.Now,
                    ExpireAt = pasteDto.ExpireAt,
                    CategoryId = pasteDto.CategoryId,
                    TypeId = pasteDto.TypeId,
                    LanguageId = pasteDto.LanguageId,
                    UserId = pasteDto.UserId
                };

                //if the creation was successful, the method will return true
                bool result = await _pasteRepository.Create(paste);

                if (result)
                {
                    Log.Information("Object {@i} created successfully.", paste.Id);
                    return true;
                }
                else
                {
                    Log.Error("Error creating object.");
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> DeletePaste(Guid id)
        {
            Paste paste = await _pasteRepository.GetById(id);

            bool awsDeletionResult = await _amazonStorage.DeleteFile(paste.BodyUrl);

            //if the deletion was successful, the method will return true
            if (awsDeletionResult)
            {
                bool result = await _pasteRepository.Delete(id);

                if (result)
                {
                    Log.Information("Object {@i} successfully deleted.", id);
                    return true;
                }
            }

            Log.Error("Object deletion error.");
            return false;
        }

        public async Task<IEnumerable<PasteDto>> GetAllPaste()
        {
            IEnumerable<Paste> pastes = await _pasteRepository.Get();

            if(pastes == null)
            {
                Log.Information("Object not found.");
                return null;
            }
            Log.Information("Received objects: {@Count}", pastes.Count());
            return _mapper.Map<IEnumerable<PasteDto>>(pastes);
        }

        public async Task<PasteDto> GetPasteById(Guid id)
        {
            Paste paste = await _pasteRepository.GetById(id);

            if(paste == null)
            {
                Log.Information("Object {@i} not found.", id);
                return null;
            }

            string pasteBody = await _amazonStorage.GetFile(paste.BodyUrl);

            var pasteDto = _mapper.Map<PasteDto>(paste);
            pasteDto.Body = pasteBody;

            return pasteDto;
        }

        public async Task<bool> UpdatePaste(PasteDto pasteDto)
        {
            Paste paste = _mapper.Map<Paste>(pasteDto);

            bool updateResult = await _amazonStorage
                .UpdateFile(pasteDto.BodyUrl, pasteDto.Body);

            //if the update was successful, the method will return true
            if (updateResult)
            {
                bool result = await _pasteRepository.Update(paste);

                if (result)
                {
                    Log.Information("Object {@i} updated.", pasteDto.Id);
                    return true;
                }
            }

            Log.Error("Object update error.");
            return false;
        }
    }
}
