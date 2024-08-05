using Microsoft.EntityFrameworkCore;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using PasteBinClone.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Persistence.Repository
{
    public class LanguageRepository(ApplicationDbContext db) : IBaseRepository<Language>
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<bool> Create(Language obj)
        {
            //Checks that the input object is not null
            if (obj == null)
            {
                return false;
            }

            _db.Languages.Add(obj);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await _db.Languages.
                FirstOrDefaultAsync(x => x.Id == id);

            //Checking if the object exists before deleting it
            if (obj != null)
            {
                _db.Languages.Remove(obj);
                await _db.SaveChangesAsync();

                //Returns true if the object was successfully deleted. 
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Language>> Get()
        {
            return await _db.Languages
                .ToListAsync();
        }

        public async Task<Language> GetById(int id)
        {
            return await _db.Languages.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> Update(Language obj)
        {
            //Checks that the input object is not null
            if (obj == null)
            {
                return false;
            }

            var language = await _db.Languages
                .FirstOrDefaultAsync(u => u.Id == obj.Id);

            //Checks if the object with this id is exists
            if (language != null)
            {
                //Separate the Language entity from the Db context
                _db.Languages.Entry(language).State = EntityState.Detached;

                _db.Languages.Update(obj);
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
