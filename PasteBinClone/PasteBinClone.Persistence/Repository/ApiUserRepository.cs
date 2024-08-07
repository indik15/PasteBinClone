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
            _db.UserPasteInfo.Add(new UserPasteInfo() {UserId = user.UserId});
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<ApiUser> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            ApiUser user =  await _db.ApiUsers
                .Include(u => u.UserPasteInfo)
                .FirstOrDefaultAsync(u => u.UserId == id);

            user.UserPasteInfo.TotalActivePastes = _db.Pastes
            .Where(u => u.UserId == user.UserId)
            .Count();

            return user;
        }
    }
}
