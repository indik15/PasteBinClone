using AutoMapper;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Services
{
    public class RatingService(IRatingRepository ratingRepository, IMapper mapper) : IRatingService
    {
        private readonly IRatingRepository _ratingRepository = ratingRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<bool> UpdatePasteRating(RatingDto ratingDto)
        {
            Rating rating = _mapper.Map<Rating>(ratingDto);

            return await _ratingRepository.Update(rating);
        }
    }
}
