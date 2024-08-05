using Microsoft.EntityFrameworkCore;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using PasteBinClone.Persistence.Data;

namespace PasteBinClone.Persistence.Repository
{
    public class CategoryRepository(ApplicationDbContext db) : IBaseRepository<Category>
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<bool> Create(Category obj)
        {
            //Checks that the input object is not null
            if (obj == null)
            {
                return false;
            }

            _db.Categories.Add(obj);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _db.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            //Checking if the object exists before deleting it
            if (obj != null)
            {
                _db.Categories.Remove(obj);
                await _db.SaveChangesAsync();

                //Returns true if the object was successfully deleted. 
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
                .ToListAsync();
        }

        public async Task<Category> GetById(int id)
        {
            return await _db.Categories.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> Update(Category obj)
        {
            //Checks that the input object is not null
            if (obj == null)
            {
                return false;
            }

            var category = await _db.Categories
                .FirstOrDefaultAsync(u => u.Id == obj.Id);

            //Checks if the object with this id is exists
            if (category != null)
            {
                category.CategoryName = obj.CategoryName;
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
