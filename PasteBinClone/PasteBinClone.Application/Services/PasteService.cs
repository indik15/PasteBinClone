using AutoMapper;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.ViewModels;
using PasteBinClone.Domain.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Application.Services
{
    public class PasteService(IPasteRepository pasteRepository, 
        IMapper mapper,
        IAmazonStorageService amazonStorage,
        IPasswordHasher passwordHasher,
        IApiUserRepository apiUser,
        IRatingRepository ratingRepository) : IPasteService
    {
        private readonly IPasteRepository _pasteRepository = pasteRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAmazonStorageService _amazonStorage = amazonStorage;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IApiUserRepository _apiUser = apiUser;
        private readonly IRatingRepository _ratingRepository = ratingRepository;

        public async Task<Guid> CreatePaste(PasteDto pasteDto)
        {
            (bool isCreate, string pasteId ) = 
                await _amazonStorage.UploadFile(pasteDto.Body);

            if (isCreate && pasteId != null)
            {

                string passwordHash = null;

                if (!pasteDto.IsPublic)
                {
                    passwordHash = _passwordHasher.PasswordHash(pasteDto.Password);
                }

                Paste paste = new()
                {
                    Id = Guid.NewGuid(),
                    Title = pasteDto.Title,
                    BodyUrl = pasteId,
                    IsPublic = pasteDto.IsPublic,
                    Password = passwordHash,
                    CreateAt = pasteDto.CreateAt,
                    ExpireAt = pasteDto.ExpireAt,
                    CategoryId = pasteDto.CategoryId,
                    TypeId = pasteDto.TypeId,
                    LanguageId = pasteDto.LanguageId,
                    UserId = pasteDto.UserId
                };

                //if the creation was successful, the method will return true
                bool result = await _pasteRepository.Create(paste);

                if (result)
                {
                    Log.Information("Object {@i} created successfully.", paste.Id);
                    return paste.Id;
                }
                else
                {
                    Log.Error("Error creating object.");
                    return Guid.Empty;
                }
            }

            return Guid.Empty;
        }

        public async Task<bool> DeletePaste(Guid id)
        {
            Paste paste = await _pasteRepository.GetById(id);

            bool awsDeletionResult = await _amazonStorage.DeleteFile(paste.BodyUrl);

            //if the deletion was successful, the method will return true
            if (awsDeletionResult)
            {
                bool result = await _pasteRepository.Delete(id);

                if (result)
                {
                    Log.Information("Object {@i} successfully deleted.", id);
                    return true;
                }
            }

            Log.Error("Object deletion error.");
            return false;
        }

        public async Task<(IEnumerable<HomePasteDto> pastes, int totalPages)> GetAllPaste(HomePasteRequestDto pasteRequestDto)
        {
            (IEnumerable<Paste> pastes, int totalPaste) = await _pasteRepository.Get(pasteRequestDto);

            List<Paste> removePasteFromDb = [];
            List<string> removePasteFromS3 = [];

            int totalPages = (int)Math.Ceiling((double)totalPaste / Constants.PasteCount);

            if (pastes == null)
            {
                Log.Information("Object not found.");
                return (null, 0);
            }

            foreach(var obj in pastes)
            {
                if (DateTime.Now > obj.ExpireAt)
                {
                    removePasteFromS3.Add(obj.BodyUrl);
                    removePasteFromDb.Add(obj);
                }
            }

            pastes.ToList().RemoveAll(p => removePasteFromDb.Contains(p));

            if (removePasteFromDb.Count > 0 && removePasteFromS3.Count > 0)
            {
                bool resultFromDb = await _pasteRepository.DeleteRange(removePasteFromDb);
                bool resultFromS3 = await _amazonStorage.DeleteRangeFiles(removePasteFromS3);

                if(!resultFromDb)
                {
                    Log.Information("Error deleting objects from the database");
                }

                if (!resultFromS3)
                {
                    Log.Information("Error deleting objects from the AWS S3");
                }
            }

            Log.Information("Received objects: {@Count}", pastes.Count());

            IEnumerable<HomePasteDto> pasteVMs = _mapper.Map<IEnumerable<HomePasteDto>>(pastes);

            return (pasteVMs, totalPages); 
        }

        public async Task<(IEnumerable<HomePasteDto> pastes, int totalPages)> GetAllUserPastes(string userId, int pageNumber)
        {
            (IEnumerable<Paste> pastes, int totalPaste) = await _pasteRepository.GetAllUserPastes(userId, pageNumber);

            int totalPages = (int)Math.Ceiling((double)totalPaste / Constants.PasteCount);

            if (pastes == null)
            {
                return (null, 0);
            }

            IEnumerable<HomePasteDto> pastesDto = _mapper.Map<IEnumerable<HomePasteDto>>(pastes);

            return (pastesDto, totalPages);
        }

        public async Task<IEnumerable<HomePasteDto>> GetFiveUserPastes(string userId)
        {
            IEnumerable<Paste> pastes = await _pasteRepository.GetFiveUserPastes(userId);

            if (pastes == null)
            {
                return null;
            }

            return _mapper.Map<IEnumerable<HomePasteDto>>(pastes);
        }

        public async Task<(GetPasteDto getPasteDto, string errorMessage)> GetPasteById(Guid id, string userId = null, string password = null)
        {
            Paste paste = await _pasteRepository.GetById(id);

            if(paste == null)
            {
                Log.Information("Object {@i} not found.", id);
                return (null, "");
            }

            if(DateTime.Now > paste.ExpireAt)
            {
                await _amazonStorage.DeleteFile(paste.BodyUrl);
                await _pasteRepository.Delete(id);

                return (null, "");
            }

            var user = await _apiUser.GetById(userId);

            if(user.Role != "Admin")
            {
                if (paste.UserId != userId)
                {
                    if (!paste.IsPublic)
                    {
                        if (string.IsNullOrEmpty(password))
                        {
                            return (new GetPasteDto { IsPublic = false }, "");
                        }
                        else
                        {
                            bool isCorrectPassword = _passwordHasher.VerifyPassword(password, paste.Password);

                            if (!isCorrectPassword)
                            {
                                return (new GetPasteDto { IsPublic = false }, "Incorrect password");
                            }
                        }
                    }
                }
            }

            string pasteBody = await _amazonStorage.GetFile(paste.BodyUrl);

            Rating rating = await _ratingRepository.Get(userId, paste.Id);
                
            var pasteDto = _mapper.Map<GetPasteDto>(paste);
            pasteDto.Body = pasteBody;
            
            if(rating != null)
            {
                pasteDto.IsLiked = rating.IsLiked;
                pasteDto.IsDisliked = rating.IsDisliked;
            }

            return (pasteDto, "");

        }

        public async Task<IEnumerable<HomePasteDto>> GetTopRatedPastes()
        {
            IEnumerable<Paste> pastes = await _pasteRepository.GetTopRatedPastes();

            if (pastes == null)
            {
                return null;
            }

            return _mapper.Map<IEnumerable<HomePasteDto>>(pastes);
        }

        public async Task<bool> UpdatePaste(PasteDto pasteDto)
        {

            var pasteFromDb = await _pasteRepository.GetById(pasteDto.Id);
            Paste paste = _mapper.Map<Paste>(pasteDto);

            paste.BodyUrl = pasteFromDb.BodyUrl;
            paste.ExpireAt = pasteFromDb.ExpireAt;

            bool updateResult = await _amazonStorage
                .UpdateFile(pasteFromDb.BodyUrl, pasteDto.Body);

            //if the update was successful, the method will return true
            if (updateResult)
            {
                bool result = await _pasteRepository.Update(paste);

                if (result)
                {
                    Log.Information("Object {@i} updated.", pasteDto.Id);
                    return true;
                }
            }

            Log.Error("Object update error.");
            return false;
        }
    }
}
