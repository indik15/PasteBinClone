﻿using AutoMapper;
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
            CreateMap<CategoryDto, CategoryVM>().ReverseMap();

            CreateMap<ContentType, ContentTypeDto>().ReverseMap();
            CreateMap<ContentTypeDto, ContentTypeVM>().ReverseMap();
        }
    }
}
