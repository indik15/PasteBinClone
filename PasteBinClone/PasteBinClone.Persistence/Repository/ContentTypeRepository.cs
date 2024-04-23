using Microsoft.EntityFrameworkCore;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;

namespace PasteBinClone.Persistence.Repository
{
    public class ContentTypeRepository : IBaseRepository<ContentType>
    {
        private readonly IApplicationDbContext _db;

        public ContentTypeRepository(IApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(ContentType obj)
        {
            _db.ContentTypes.Add(obj);
            await _db.SaveChangesAsync();
        }

        public async Task<int?> Delete(int id)
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

                //If the object is successfully deleted the object id is returned 
                return obj.Id;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<ContentType>> Get()
        {
            return await _db.ContentTypes
                .AsNoTracking()
                .ToArrayAsync();         
        }

        public async Task<ContentType> GetById(int id)
        {
            return await _db.ContentTypes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<int?> Update(ContentType obj)
        {
            //Checks that the input object is not null
            if (obj == null)
            {
                return null;
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

                    //Return the id if the object exists and was updated 
                    return contentType.Id;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
