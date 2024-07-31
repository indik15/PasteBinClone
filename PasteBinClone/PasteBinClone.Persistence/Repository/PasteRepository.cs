﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.EntityFrameworkCore;
using PasteBinClone.Application;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PasteBinClone.Persistence.Repository
{
    public class PasteRepository(IApplicationDbContext db) : IPasteRepository
    {
        private readonly IApplicationDbContext _db = db;

        public async Task<bool> Create(Paste paste)
        {
            //Checks that the input object is not null
            if (paste == null)
            {
                return false;
            }

            _db.Pastes.Add(paste);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var paste = await _db.Pastes
                .FirstOrDefaultAsync(u => u.Id == id);

            //Checking if the object exists before deleting it
            if (paste != null)
            {
                //Separate the Paste entity from the Db context
                _db.Pastes.Entry(paste).State = EntityState.Deleted;

                _db.Pastes.Remove(paste);
                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRange(IEnumerable<Paste> pastes)
        {
            if(pastes != null)
            {

                foreach(var paste in pastes)
                {
                    //Separate the Paste entity from the Db context
                    _db.Pastes.Entry(paste).State = EntityState.Deleted;
                }

                _db.Pastes.RemoveRange(pastes);
                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<(IEnumerable<Paste> pastes, int totalPaste)> Get(
            int pageNumber,
            int? typeFilter,
            int? categoryFilter,
            int? languageFilter,
            int? sortedByFilter)
        {
            var query = _db.Pastes
                .AsNoTracking()
                .Include(u => u.Category)
                .Include(u => u.Language)
                .Include(u => u.Type)
                .AsQueryable();
                
            if(typeFilter != 0 && typeFilter != null)
            {
                query = query.Where(u => u.TypeId == typeFilter);
            }
            if (categoryFilter != 0 && categoryFilter != null)
            {
                query = query.Where(u => u.CategoryId == categoryFilter);
            }
            if (languageFilter != 0 && languageFilter != null)
            {
                query = query.Where(u => u.LanguageId == languageFilter);
            }

            if (sortedByFilter != 0 && sortedByFilter != null)
            {
                if (sortedByFilter == 1)
                {
                    query = query.OrderByDescending(u => u.CreateAt);
                }
                else if (sortedByFilter == 2)
                {
                    query = query.OrderBy(u => u.CreateAt);
                }
                else if (sortedByFilter == 3)
                {
                    query = query.OrderByDescending(u => u.Likes);
                }
            }

            int totalPastes = query.Count();

            IEnumerable<Paste> paste = await query
                .Skip((pageNumber - 1) * Constants.PasteCount)
                .Take(Constants.PasteCount)
                .ToListAsync();

            return (paste, totalPastes);
        }

        public async Task<(IEnumerable<Paste> pastes, int totalPaste)> GetAllUserPastes(string userId, int pageNumber)
        {
            var query = _db.Pastes
                .Include(u => u.Type)
                .Where(u => u.UserId == userId)
                .AsQueryable();

            int totalPastes = query.Count();

            IEnumerable<Paste> paste = await query
                .Skip((pageNumber - 1) * Constants.PasteCount)
                .Take(Constants.PasteCount)
                .ToListAsync();

            return (paste, totalPastes);
        }

        public async Task<Paste> GetById(Guid id)
        {
            return await _db.Pastes
            .Include(p => p.Category)
            .Include(p => p.Language)
            .Include(p => p.Type)
            .Include(p => p.User)
            .Include(p => p.Comments)
                .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Paste>> GetFiveUserPastes(string userId)
        {
            return await _db.Pastes
                .Include(u => u.Type)
                .Where(u => u.UserId == userId)
                .Take(5)
                .ToListAsync();
        }

        public async Task<IEnumerable<Paste>> GetTopRatedPastes()
        {
            return await _db.Pastes
                .Include(u => u.Type)
                .OrderByDescending(u => u.Likes)
                .Where(u => u.Likes != 0)
                .Take(5)
                .ToListAsync();
        }

        public async Task<bool> Update(Paste paste)
        {
            //Checks that the input object is not null
            if (paste == null)
            {
                return false;
            }

            var currentPaste = await _db.Pastes
                .FirstOrDefaultAsync(u => u.Id == paste.Id);

            if(currentPaste != null)
            {
                //Separate the Paste entity from the Db context
                _db.Pastes.Entry(currentPaste).State = EntityState.Detached;

                paste.Likes = currentPaste.Likes;
                paste.Dislikes = currentPaste.Dislikes;

                _db.Pastes.Update(paste);
                await _db.SaveChangesAsync();

                //Return true if the object exists and was updated 
                return true;
            }

            return false;
        }
    }
}
