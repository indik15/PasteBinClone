using Microsoft.EntityFrameworkCore;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using PasteBinClone.Persistence.Data;

namespace PasteBinClone.Persistence.Repository
{
    public class ContentTypeRepository(ApplicationDbContext db) : IBaseRepository<ContentType>
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<bool> Create(ContentType obj)
        {
            //Checks that the input object is not null
            if (obj == null)
            {
                return false;
            }

            _db.ContentTypes.Add(obj);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _db.ContentTypes
                .FirstOrDefaultAsync(u => u.Id == id);

            //Checking if the object exists before deleting it
            if (obj != null)
            {
                //Separate the contentType entity from the Db context
                _db.ContentTypes.Entry(obj).State = EntityState.Deleted;

                _db.ContentTypes.Remove(obj);
                await _db.SaveChangesAsync();

                //Returns true if the object was successfully deleted. 
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<ContentType>> Get()
        {
            return await _db.ContentTypes
                .ToListAsync();         
        }

        public async Task<ContentType> GetById(int id)
        {
            return await _db.ContentTypes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> Update(ContentType obj)
        {
            //Checks that the input object is not null
            if (obj == null)
            {
                return false;
            }
            else
            {
                var contentType = await _db.ContentTypes
                    .FirstOrDefaultAsync(u => u.Id == obj.Id);

                //Checks if the object with this id is exists
                if (contentType != null)
                {
                    //Separate the contentType entity from the Db context
                    _db.ContentTypes.Entry(contentType).State = EntityState.Detached;

                    _db.ContentTypes.Update(obj);
                    await _db.SaveChangesAsync();

                    //Return true if the object exists and was updated 
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
