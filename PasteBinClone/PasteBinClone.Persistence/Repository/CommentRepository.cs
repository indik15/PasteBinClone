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

        public async Task<IEnumerable<Comment>> Get()
        {
            return await _db.Comments
                .Include(u => u.UserId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Comment> GetById(Guid id)
        {
            return await _db.Comments.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> Update(Comment obj)
        {
            //Checks that the input object is not null
            if (obj == null)
            {
                return false;
            }

            var comment = await _db.Comments.FirstOrDefaultAsync(u => u.Id == obj.Id);

            //Checks if the object with this id is exists
            if (comment != null)
            {
                //Separate the Comment entity from the Db context
                _db.Comments.Entry(comment).State = EntityState.Detached;

                _db.Comments.Update(obj);
                await _db.SaveChangesAsync();

                //Return true if the object exists and was updated 
                return true;
            }

            return false;
        }
    }
}
