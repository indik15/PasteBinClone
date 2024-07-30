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

        public async Task<Rating> Get(string userId, Guid pasteId)
        {
            return await _db.Ratings.FirstOrDefaultAsync(u => u.PasteId == pasteId && u.UserId == userId);
        }

        public async Task<bool> Update(Rating rating)
        {
            if(rating == null)
            {
                return false;
            }

            await using var transaction = await _db.Database.BeginTransactionAsync();

            try
            {
                Rating? currentRating = await _db.Ratings
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.UserId == rating.UserId && u.PasteId == rating.PasteId);

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
                        currentRating.IsLiked = true;
                        currentRating.IsDisliked = false;

                    }
                    else if (rating.IsDisliked && currentRating.IsLiked)
                    {
                        paste.Likes--;
                        paste.Dislikes++;
                        currentRating.IsDisliked = true;
                        currentRating.IsLiked = false;
                    }
                    else if (rating.IsDisliked && currentRating.IsDisliked)
                    {
                        paste.Dislikes--;
                        currentRating.IsDisliked = false;
                    }
                    else if (rating.IsLiked && currentRating.IsLiked)
                    {
                        paste.Likes--;
                        currentRating.IsLiked = false;
                    }
                    else if ((!currentRating.IsLiked && !currentRating.IsDisliked) && rating.IsLiked)
                    {
                        paste.Likes++;
                        currentRating.IsLiked = true;
                    }
                    else if ((!currentRating.IsDisliked && !currentRating.IsLiked) && rating.IsDisliked)
                    {
                        paste.Dislikes++;
                        currentRating.IsDisliked = true;
                    }

                    _db.Ratings.Update(currentRating);

                    await _db.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
            }

            return true;
        }
    }
}
