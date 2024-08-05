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
    public class ApiUserRepository(ApplicationDbContext db) : IApiUserRepository
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<bool> Create(ApiUser user)
        {
            if(user == null)
            {
                return false;
            }

            _db.ApiUsers.Add(user);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<ApiUser> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return await _db.ApiUsers.FirstOrDefaultAsync(u => u.UserId == id);
        }
    }
}
