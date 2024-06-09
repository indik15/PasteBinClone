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
        IMapper mapper) : IPasteService
    {
        private readonly IPasteRepository _pasteRepository = pasteRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> CreatePaste(PasteDto pasteDto)
        {
            Paste paste = new()
            {
                Title = pasteDto.Title,
                BodyUrl = "",
                IsPublic = false,
                LifeTime = DateTime.Now,
                CategoryId = 0,
                TypeId = 0,
                LanguageId = 0,
                UserId = ""
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

        public async Task<bool> DeletePaste(Guid id)
        {
            //if the deletion was successful, the method will return true
            bool result = await _pasteRepository.Delete(id);

            if (result)
            {
                Log.Information("Object {@i} successfully deleted.", id);
                return true;
            }
            else
            {
                Log.Error("Object deletion error.");
                return false;
            }
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

            return _mapper.Map<PasteDto>(paste);
        }

        public async Task<bool> UpdatePaste(PasteDto pasteDto)
        {
            Paste paste = _mapper.Map<Paste>(pasteDto);

            //if the update was successful, the method will return true
            bool result = await _pasteRepository.Update(paste);

            if (result)
            {
                Log.Information("Object {@i} updated.", pasteDto.Id);
                return true;
            }

            Log.Error("Object update error.");
            return false;
        }
    }
}
