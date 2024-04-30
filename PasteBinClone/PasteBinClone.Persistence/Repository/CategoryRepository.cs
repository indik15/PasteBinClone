﻿using Microsoft.EntityFrameworkCore;
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
                //Separate the Category entity from the Db context
                _db.Categories.Entry(obj).State = EntityState.Deleted; 

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
                .AsNoTracking()
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
                //Separate the Category entity from the Db context
                _db.Categories.Entry(category).State = EntityState.Detached;

                _db.Categories.Update(obj);
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
