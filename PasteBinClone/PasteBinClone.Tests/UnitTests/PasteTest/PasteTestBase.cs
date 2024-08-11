using AutoMapper;
using Moq;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.Services;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Tests.UnitTests.PasteTest
{
    public class PasteTestBase
    {
        protected readonly Mock<IMapper> _mapperMock;
        protected readonly Mock<IPasteRepository> _pasteRepositoryMock;
        protected readonly Mock<IAmazonStorageService> _amazonS3Mock;
        protected readonly Mock<IPasswordHasher> _hasherMock;
        protected readonly Mock<IApiUserRepository> _userRepositoryMock;
        protected readonly Mock<IRatingRepository> _ratingRepositoryMock;
        protected readonly PasteService _pasteService;

        public PasteTestBase()
        {
            _mapperMock = new();
            _pasteRepositoryMock = new();
            _amazonS3Mock = new();
            _hasherMock = new();
            _userRepositoryMock = new();
            _ratingRepositoryMock = new();

            _pasteService = new PasteService(
                _pasteRepositoryMock.Object,
                _mapperMock.Object,
                _amazonS3Mock.Object,
                _hasherMock.Object,
                _userRepositoryMock.Object,
                _ratingRepositoryMock.Object);
        }

        protected static Paste PublicPaste => new Paste
        {
            Id = Guid.NewGuid(),
            Title = "Test",
            BodyUrl = "Url",
            IsPublic = true,
            Password = null,
            CreateAt = DateTime.Now,
            ExpireAt = DateTime.Now.AddMinutes(30),
            CategoryId = 1,
            LanguageId = 1,
            TypeId = 1,
            UserId = Guid.NewGuid().ToString(),
        };

        protected static PasteDto PublicPasteDto => new PasteDto
        {
            Id = Guid.NewGuid(),
            Title = "Test",
            Body = "TestPublicBody",
            IsPublic = true,
            Password = null,
            CreateAt = DateTime.Now,
            ExpireAt = DateTime.Now.AddMinutes(30),
            CategoryId = 1,
            LanguageId = 1,
            TypeId = 1,
            UserId = Guid.NewGuid().ToString(),
        };

        protected static PasteDto PrivatePasteDto => new PasteDto
        {
            Id = Guid.NewGuid(),
            Title = "Test",
            Body = "TestPrivateBody",
            IsPublic = false,
            Password = "123",
            CreateAt = DateTime.Now,
            ExpireAt = DateTime.Now.AddMinutes(30),
            CategoryId = 1,
            LanguageId = 1,
            TypeId = 1,
            UserId = Guid.NewGuid().ToString(),
        };

        protected static PasteDto PrivatePasteDtoNullPassword => new PasteDto
        {
            Id = Guid.NewGuid(),
            Title = "Test",
            Body = "TestPrivateBody",
            IsPublic = false,
            Password = null,
            CreateAt = DateTime.Now,
            ExpireAt = DateTime.Now.AddMinutes(30),
            CategoryId = 1,
            LanguageId = 1,
            TypeId = 1,
            UserId = Guid.NewGuid().ToString(),
        };

        protected static IEnumerable<PasteDto> PasteDtoList => new List<PasteDto>
        {
            new PasteDto
            {
                Id = Guid.NewGuid(),
                Title = "Test1",
                Body = "TestPublicBody",
                IsPublic = true,
                Password = null,
                CreateAt = DateTime.Now,
                ExpireAt = DateTime.Now.AddMinutes(30),
                CategoryId = 1,
                LanguageId = 1,
                TypeId = 1,
                UserId = Guid.NewGuid().ToString()
            },
            new PasteDto
            {
                Id = Guid.NewGuid(),
                Title = "Test2",
                Body = "TestPublicBody",
                IsPublic = true,
                Password = null,
                CreateAt = DateTime.Now,
                ExpireAt = DateTime.Now.AddMinutes(30),
                CategoryId = 1,
                LanguageId = 1,
                TypeId = 1,
                UserId = Guid.NewGuid().ToString()
            },

        };

        protected static IEnumerable<Paste> PasteList => new List<Paste>
        {
            new Paste
            {
                Id = Guid.NewGuid(),
                Title = "Test1",
                BodyUrl = Guid.NewGuid().ToString(),
                IsPublic = true,
                Password = null,
                CreateAt = DateTime.Now,
                ExpireAt = DateTime.Now.AddMinutes(30),
                CategoryId = 1,
                LanguageId = 1,
                TypeId = 1,
                UserId = Guid.NewGuid().ToString()
            },
            new Paste
            {
                Id = Guid.NewGuid(),
                Title = "Test2",
                BodyUrl = Guid.NewGuid().ToString(),
                IsPublic = true,
                Password = null,
                CreateAt = DateTime.Now,
                ExpireAt = DateTime.Now.AddMinutes(30),
                CategoryId = 1,
                LanguageId = 1,
                TypeId = 1,
                UserId = Guid.NewGuid().ToString()
            }
        };
    }
}
