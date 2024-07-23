using Microsoft.EntityFrameworkCore;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Persistence.Repository
{
    public class RatingRepository(IApplicationDbContext db) : IRatingRepository
    {
        private readonly IApplicationDbContext _db = db;

        public async Task<bool> Update(Rating rating)
        {
            if(rating == null)
            {
                return false;
            }

            Rating? currentRating = await _db.Ratings
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == rating.UserId && u.PasteId == u.PasteId);

            Paste paste = _db.Pastes.FirstOrDefault(u => u.Id == rating.PasteId);

            if (paste == null)
                return false;

            if (currentRating == null)
            {
                _db.Ratings.Add(rating);

                if (rating.IsLiked)
                    paste.Likes++;

                else if (rating.IsDisliked)
                    paste.Dislikes++;
            }
            else
            {
                if (rating.IsLiked && currentRating.IsDisliked)
                {
                    paste.Likes++;
                    paste.Dislikes--;
                }
                else if (rating.IsDisliked && currentRating.IsLiked)
                {
                    paste.Likes--;
                    paste.Dislikes++;
                }
                else if (rating.IsDisliked && currentRating.IsDisliked)
                {
                    paste.Dislikes--;
                }
                else if (rating.IsLiked && currentRating.IsLiked)
                {
                    paste.Likes--;
                }
                _db.Ratings.Update(rating);
            }

            await _db.SaveChangesAsync();

            return true;
        }
    }
}
