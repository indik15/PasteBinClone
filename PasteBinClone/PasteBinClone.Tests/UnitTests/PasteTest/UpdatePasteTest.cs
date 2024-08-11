using AutoMapper;
using FluentAssertions;
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
    public class UpdatePasteTest
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IPasteRepository> _pasteRepositoryMock;
        private readonly Mock<IAmazonStorageService> _amazonS3Mock;
        private readonly Mock<IPasswordHasher> _hasherMock;
        private readonly Mock<IApiUserRepository> _userRepositoryMock;
        private readonly Mock<IRatingRepository> _ratingRepositoryMock;
        private readonly PasteService _pasteService;

        public UpdatePasteTest()
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

        [Fact]
        public async Task UpdatePaste_SuccessResult_ReturnsTrue()
        {
            //Arrange
            _mapperMock.Setup(u => u.Map<Paste>(It.IsAny<PasteDto>()))
                .Returns(PublicPaste);

            _pasteRepositoryMock.Setup(u => u.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(PublicPaste);

            _pasteRepositoryMock.Setup(u => u.Update(It.IsAny<Paste>()))
                .ReturnsAsync(true);

            _amazonS3Mock.Setup(u => u.UpdateFile(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            //Act
            var result = await _pasteService.UpdatePaste(PublicPasteDto);

            //Assert
            result.Should().BeTrue();

        }

        [Fact]
        public async Task UpdatePaste_WithFalseResultFromAmazonS3_ReturnsFalse()
        {
            //Arrange
            _mapperMock.Setup(u => u.Map<Paste>(It.IsAny<PasteDto>()))
                .Returns(PublicPaste);

            _pasteRepositoryMock.Setup(u => u.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(PublicPaste);

            _pasteRepositoryMock.Setup(u => u.Update(It.IsAny<Paste>()))
                .ReturnsAsync(true);

            _amazonS3Mock.Setup(u => u.UpdateFile(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            //Act
            var result = await _pasteService.UpdatePaste(PublicPasteDto);

            //Assert
            result.Should().BeFalse();

        }

        [Fact]
        public async Task UpdatePaste_WithNullResultFromGetByIdInRepository_ReturnsFalse()
        {
            //Arrange

            _pasteRepositoryMock.Setup(u => u.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((Paste)null);
            //Act
            var result = await _pasteService.UpdatePaste(PublicPasteDto);

            //Assert
            result.Should().BeFalse();

        }

        [Fact]
        public async Task UpdatePaste_WithFalseResultFromUpdateInRepository_ReturnsFalse()
        {
            //Arrange
            _mapperMock.Setup(u => u.Map<Paste>(It.IsAny<PasteDto>()))
                .Returns(PublicPaste);

            _pasteRepositoryMock.Setup(u => u.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(PublicPaste);

            _pasteRepositoryMock.Setup(u => u.Update(It.IsAny<Paste>()))
                .ReturnsAsync(false);

            _amazonS3Mock.Setup(u => u.UpdateFile(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            //Act
            var result = await _pasteService.UpdatePaste(PublicPasteDto);

            //Assert
            result.Should().BeFalse();

        }

        private static Paste PublicPaste => new Paste
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

        private static PasteDto PublicPasteDto => new PasteDto
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
    }
}
