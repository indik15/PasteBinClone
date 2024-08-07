using AutoMapper;
using FluentAssertions;
using FluentAssertions.Execution;
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
    public class GetAllPasteTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPasteRepository> _pasteRepositoryMock;
        private readonly Mock<IAmazonStorageService> _amazonS3Mock;
        private readonly Mock<IPasswordHasher> _hasherMock;
        private readonly Mock<IApiUserRepository> _userRepositoryMock;
        private readonly Mock<IRatingRepository> _ratingRepositoryMock;
        private readonly PasteService _pasteService;

        public GetAllPasteTest()
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
        #region GetPasteTests

        [Fact]
        public async Task GetAllPaste_SuccessResult_ReturnsCollectionOfPastes()
        {
            //Arrange
            _pasteRepositoryMock.Setup(u => u.Get(It.IsAny<HomePasteRequestDto>()))
                .ReturnsAsync((PasteList, 15));

            _pasteRepositoryMock.Setup(u => u.DeleteRange(It.IsAny<IEnumerable<Paste>>()))
                .ReturnsAsync(true);

            _mapperMock.Setup(u => u.Map<IEnumerable<PasteDto>>(PasteDtoList))
                .Returns(PasteDtoList);

            _amazonS3Mock.Setup(u => u.DeleteRangeFiles(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(true);
            //Act

            var result = await _pasteService.GetAllPaste(new HomePasteRequestDto { });

            //Assert

            //Combining checks into 1 block that will be executed at the same time
            using AssertionScope _ = new();

            result.pastes.Should().NotBeNull();
            result.totalPages.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetAllPaste_WithNullResultFromRepository_ReturnsNullAndZero()
        {
            //Arrange
            _pasteRepositoryMock.Setup(u => u.Get(It.IsAny<HomePasteRequestDto>()))
                .ReturnsAsync((null, 15));

            _pasteRepositoryMock.Setup(u => u.DeleteRange(It.IsAny<IEnumerable<Paste>>()))
                .ReturnsAsync(true);

            _mapperMock.Setup(u => u.Map<IEnumerable<PasteDto>>(PasteDtoList))
                .Returns(PasteDtoList);

            _amazonS3Mock.Setup(u => u.DeleteRangeFiles(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(true);
            //Act

            var result = await _pasteService.GetAllPaste(new HomePasteRequestDto { });

            //Assert

            //Combining checks into 1 block that will be executed at the same time
            using AssertionScope _ = new();

            result.pastes.Should().BeNull();
            result.totalPages.Should().Be(0);
        }

        [Fact]
        public async Task GetAllPaste_SuccessResultWithExpireTimeInPaste_ReturnsCollectionOfPastes()
        {
            //Arrange

            //The result will be one post with an expired lifetime limit
            _pasteRepositoryMock.Setup(u => u.Get(It.IsAny<HomePasteRequestDto>()))
                .ReturnsAsync((new List<Paste> { new Paste { ExpireAt = new DateTime(2024, 6, 16)} }, 15));

            _pasteRepositoryMock.Setup(u => u.DeleteRange(It.IsAny<IEnumerable<Paste>>()))
                .ReturnsAsync(true);

            _mapperMock.Setup(u => u.Map<IEnumerable<PasteDto>>(PasteDtoList))
                .Returns(PasteDtoList);

            _amazonS3Mock.Setup(u => u.DeleteRangeFiles(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(true);
            //Act

            var result = await _pasteService.GetAllPaste(new HomePasteRequestDto { });

            //Assert

            //Combining checks into 1 block that will be executed at the same time
            using AssertionScope _ = new();

            result.pastes.Should().NotBeNull();
            result.totalPages.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetAllPaste_WithFalseResultFromDeleteMethodInRepository_ReturnsNull()
        {
            //Arrange

            //The result will be one post with an expired lifetime limit
            _pasteRepositoryMock.Setup(u => u.Get(It.IsAny<HomePasteRequestDto>()))
                .ReturnsAsync((new List<Paste> { new Paste { ExpireAt = new DateTime(2024, 6, 16) } }, 15));

            _pasteRepositoryMock.Setup(u => u.DeleteRange(It.IsAny<IEnumerable<Paste>>()))
                .ReturnsAsync(false);

            _mapperMock.Setup(u => u.Map<IEnumerable<PasteDto>>(PasteDtoList))
                .Returns(PasteDtoList);

            _amazonS3Mock.Setup(u => u.DeleteRangeFiles(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(true);
            //Act

            var result = await _pasteService.GetAllPaste(new HomePasteRequestDto { });

            //Assert

            //Combining checks into 1 block that will be executed at the same time
            using AssertionScope _ = new();

            result.pastes.Should().BeNull();
            result.totalPages.Should().Be(0);
        }

        [Fact]
        public async Task GetAllPaste_WithFalseResultFromDeleteMethodInAmazonS3_ReturnsNull()
        {
            //Arrange

            //The result will be one post with an expired lifetime limit
            _pasteRepositoryMock.Setup(u => u.Get(It.IsAny<HomePasteRequestDto>()))
                .ReturnsAsync((new List<Paste> { new Paste { ExpireAt = new DateTime(2024, 6, 16) } }, 15));

            _pasteRepositoryMock.Setup(u => u.DeleteRange(It.IsAny<IEnumerable<Paste>>()))
                .ReturnsAsync(true);

            _mapperMock.Setup(u => u.Map<IEnumerable<PasteDto>>(PasteDtoList))
                .Returns(PasteDtoList);

            _amazonS3Mock.Setup(u => u.DeleteRangeFiles(It.IsAny<IEnumerable<string>>()))
                .ReturnsAsync(false);
            //Act

            var result = await _pasteService.GetAllPaste(new HomePasteRequestDto { });

            //Assert

            //Combining checks into 1 block that will be executed at the same time
            using AssertionScope _ = new();

            result.pastes.Should().BeNull();
            result.totalPages.Should().Be(0);
        }
        #endregion

        private static IEnumerable<PasteDto> PasteDtoList => new List<PasteDto>
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

        private static IEnumerable<Paste> PasteList => new List<Paste>
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
