﻿using PasteBinClone.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IPasteService
    {
        Task<bool> CreatePaste(PasteDto pasteDto);
        Task<PasteDto> GetPasteById(Guid id);
        Task<IEnumerable<PasteDto>> GetAllPaste();
        Task<bool> DeletePaste(Guid id);
        Task<bool> UpdatePaste(PasteDto pasteDto);
    }
}
