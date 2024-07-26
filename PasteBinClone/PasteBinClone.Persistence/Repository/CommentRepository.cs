using Microsoft.EntityFrameworkCore;
using PasteBinClone.Application;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Persistence.Repository
{
    public class CommentRepository(IApplicationDbContext db) : ICommentRepository
    {
        private readonly IApplicationDbContext _db = db;

        public async Task<bool> Create(Comment obj)
        {
            //Checks that the input object is not null
            if (obj == null)
            {
                return false;
            }

            _db.Comments.Add(obj);

            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var comment = await _db.Comments.FirstOrDefaultAsync(u => u.Id == id);

            //Checking if the object exists before deleting it
            if (comment != null)
            {
                //Separate the Comment entity from the Db context
                _db.Comments.Entry(comment).State = EntityState.Deleted;

                _db.Comments.Remove(comment);
                await _db.SaveChangesAsync();

                //Returns true if the object was successfully deleted. 
                return true;
            }
            return false;
        }

        public async Task<(IEnumerable<Comment> comments, int totalComments)> Get(Guid pasteId, int page)
        {
            IEnumerable<Comment> comments = await _db.Comments
                .Where(u => u.PasteId == pasteId)
                .Include(u => u.User)
                .Skip((page - 1) * Constants.CommentCount)
                .Take(Constants.CommentCount)
                .ToListAsync();

            int totalComments = _db.Comments.Count(u => u.PasteId == pasteId);

            return (comments, totalComments);
        }
    }
}
