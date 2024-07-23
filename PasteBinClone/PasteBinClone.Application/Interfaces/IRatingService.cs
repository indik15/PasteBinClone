using PasteBinClone.Application.Dto;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Interfaces
{
    public interface IRatingService
    {
        Task<bool> UpdatePasteRating(RatingDto rating);
    }
}
