using Microsoft.EntityFrameworkCore;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using PasteBinClone.Persistence.Data;

namespace PasteBinClone.Persistence.Repository
{
    public class CategoryRepository : IBaseRepository<Category>
    {
        private readonly IApplicationDbContext _db;

        public CategoryRepository(IApplicationDbContext db)
        {
            _db = db;     
        }

        public async Task Create(Category obj)
        {
            _db.Categories.Add(obj);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _db.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if(obj != null)
            {
                _db.Categories.Entry(obj).State = EntityState.Deleted; 

                _db.Categories.Remove(obj);
                await _db.SaveChangesAsync();

                return true;
            }

            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Category>> Get()
        {
            return await _db.Categories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> Update(Category obj)
        {
            var category = await _db.Categories
                .FirstOrDefaultAsync(u => u.Id == obj.Id);

            if(category != null)
            {
                _db.Categories.Entry(category).State = EntityState.Detached;

                _db.Categories.Update(obj);
                await _db.SaveChangesAsync();

                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
