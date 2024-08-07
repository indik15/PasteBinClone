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
            //Category mappings
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryVM>().ReverseMap();
            CreateMap<CategoryDto, CategoryVM>().ReverseMap();

            //ContentType mappings
            CreateMap<ContentType, ContentTypeDto>().ReverseMap();
            CreateMap<ContentType, ContentTypeVM>().ReverseMap();
            CreateMap<ContentTypeDto, ContentTypeVM>().ReverseMap();

            //Language mappings
            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<Language, LanguageVM>().ReverseMap();
            CreateMap<LanguageDto, LanguageVM>().ReverseMap();

            //Paste mappings
            CreateMap<Paste, HomePasteDto>()
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.Type))
                .ReverseMap();
            CreateMap<Paste, PasteDto>().ReverseMap();
            CreateMap<Paste, GetPasteDto>().ReverseMap();
            CreateMap<HomePasteVM, HomePasteDto>().ReverseMap();
            CreateMap<GetPasteVM, GetPasteDto>().ReverseMap();

            //Comment mappings
            CreateMap<Comment, CommentDto>()
                .ReverseMap()
                .ForMember(comment => comment.User, opt => opt.Ignore());
            CreateMap<Comment, CommentVM>().ReverseMap();
            CreateMap<CommentDto, CommentVM>();

            //Rating mappings
            CreateMap<Rating, RatingDto>().ReverseMap();

            //ApiUser mappings
            CreateMap<ApiUser, ApiUserVM>().ReverseMap();
        }
    }
}
