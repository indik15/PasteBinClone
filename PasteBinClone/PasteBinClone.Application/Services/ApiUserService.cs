using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.ViewModels;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Services
{
    public class ApiUserService(IApiUserRepository userRepository) : IApiUserService
    {
        private readonly IApiUserRepository _userRepository = userRepository;

        public async Task<bool> CreateUser(ApiUser user) => 
            await _userRepository.Create(user) ? true : false;

        public async Task<ApiUser> GetApiUserById(string id) =>
            await _userRepository.GetById(id);

        public async Task<ApiUser> GetApiUserByIdWithUserPasteInfo(string id) =>
            await _userRepository.GetByIdWithUserPasteInfo(id);
    }
}
