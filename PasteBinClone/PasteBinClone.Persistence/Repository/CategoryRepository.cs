﻿using Microsoft.EntityFrameworkCore;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using PasteBinClone.Persistence.Data;

namespace PasteBinClone.Persistence.Repository
{
    public class CategoryRepository : IBaseRepository<Category>
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;     
        }

        public async Task<bool> Create(Category obj)
        {
            _db.Add(obj);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _db.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if(obj != null)
            {
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
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == obj.Id);

            if(category != null)
            {
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
