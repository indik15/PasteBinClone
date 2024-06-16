using AutoMapper;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.ViewModels;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Mappings
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<CategoryDto, CategoryVM>().ReverseMap();

            CreateMap<ContentType, ContentTypeDto>().ReverseMap();
            CreateMap<ContentType, ContentTypeVM>().ReverseMap();
            CreateMap<ContentTypeDto, ContentTypeVM>().ReverseMap();

            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<Language, LanguageVM>().ReverseMap();
            CreateMap<LanguageDto, LanguageVM>().ReverseMap();

            CreateMap<Paste, PasteDto>().ReverseMap();
            CreateMap<PasteDto, PasteVM>().ReverseMap();
        }
    }
}
