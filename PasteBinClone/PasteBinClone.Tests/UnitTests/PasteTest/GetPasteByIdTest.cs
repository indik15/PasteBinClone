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
    public class GetPasteByIdTest : PasteTestBase
    {
        [Fact]
        public async Task GetPasteById_SuccessResult_ReturnsPaste()
        {
            //Arrange
            _pasteRepositoryMock.Setup(u => u.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(PublicPaste);

            _mapperMock.Setup(u => u.Map<GetPasteDto>(It.IsAny<Paste>()))
                .Returns(new GetPasteDto { UserId = "123"});

            _amazonS3Mock.Setup(u => u.GetFile(It.IsAny<string>()))
                .ReturnsAsync("PasteBody");

            _userRepositoryMock.Setup(u => u.GetById(It.IsAny<string>()))
                .ReturnsAsync(new ApiUser { UserId = "17ADE1AC-666B-4DAE-83EF-0502728C7CEC", Role = "User"});

            //Act
            var result = await _pasteService.GetPasteById(Guid.NewGuid());

            //Assert
            result.getPasteDto.Should().NotBeNull();
        }


        [Fact]
        public async Task GetPasteById_WithNullResultFromPasteRepository_ReturnsNull()
        {
            //Arrange
            _pasteRepositoryMock.Setup(u => u.GetById(It.IsAny<Guid>()))
                .ReturnsAsync((Paste)null);

            _pasteRepositoryMock.Setup(u => u.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);

            _mapperMock.Setup(u => u.Map<Paste>(PublicPasteDto))
                .Returns(PublicPaste);

            _amazonS3Mock.Setup(u => u.DeleteFile(It.IsAny<string>()))
                .ReturnsAsync(true);

            _userRepositoryMock.Setup(u => u.GetById(It.IsAny<string>()))
                .ReturnsAsync(new ApiUser { Role = "User" });
            //Act
            var result = await _pasteService.GetPasteById(Guid.NewGuid());

            //Assert
            result.getPasteDto.Should().BeNull();
        }


        [Fact]
        public async Task GetPasteById_WithExpireTimeInPaste_ReturnsNull()
        {
            //Arrange

            Paste testPaste = new Paste { ExpireAt = new DateTime(2024, 6, 16) };

            //The result will be one post with an expired lifetime limit
            _pasteRepositoryMock.Setup(u => u.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(testPaste);

            _mapperMock.Setup(u => u.Map<GetPasteDto>(It.IsAny<Paste>()))
                .Returns(new GetPasteDto { UserId = "123" });

            _amazonS3Mock.Setup(u => u.GetFile(It.IsAny<string>()))
                .ReturnsAsync("PasteBody");

            _userRepositoryMock.Setup(u => u.GetById(It.IsAny<string>()))
                .ReturnsAsync(new ApiUser { UserId = "17ADE1AC-666B-4DAE-83EF-0502728C7CEC", Role = "User" });

            //Act
            var result = await _pasteService.GetPasteById(Guid.NewGuid());

            //Assert
            result.getPasteDto.Should().BeNull();
        }

        [Fact]
        public async Task GetPasteById_WithPrivatePasteAndEmptyPassword_ReturnsIsPublicFalse()
        {
            //Arrange

            Paste testPaste = new Paste { IsPublic = false, ExpireAt = DateTime.Now.AddMinutes(30) };

            _pasteRepositoryMock.Setup(u => u.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(testPaste);

            _mapperMock.Setup(u => u.Map<GetPasteDto>(It.IsAny<Paste>()))
                .Returns(new GetPasteDto { UserId = "123" });

            _amazonS3Mock.Setup(u => u.GetFile(It.IsAny<string>()))
                .ReturnsAsync("PasteBody");

            _userRepositoryMock.Setup(u => u.GetById(It.IsAny<string>()))
                .ReturnsAsync(new ApiUser { UserId = "17ADE1AC-666B-4DAE-83EF-0502728C7CEC", Role = "User" });

            //Act
            var result = await _pasteService.GetPasteById(Guid.NewGuid());

            //Assert

            //Combining checks into 1 block that will be executed at the same time
            using AssertionScope _ = new();

            result.getPasteDto.IsPublic.Should().BeFalse();
            result.errorMessage.Should().BeEmpty();
        }

        [Fact]
        public async Task GetPasteById_WithPrivatePasteAndIncorrectPassword_ReturnsIsPublicFalseAndErrorMessage()
        {
            //Arrange

            Paste testPaste = new Paste
            {
                IsPublic = false,
                ExpireAt = DateTime.Now.AddMinutes(30)
            };

            _pasteRepositoryMock.Setup(u => u.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(testPaste);

            _mapperMock.Setup(u => u.Map<GetPasteDto>(It.IsAny<Paste>()))
                .Returns(new GetPasteDto { UserId = "123" });

            _amazonS3Mock.Setup(u => u.GetFile(It.IsAny<string>()))
                .ReturnsAsync("PasteBody");

            _userRepositoryMock.Setup(u => u.GetById(It.IsAny<string>()))
                .ReturnsAsync(new ApiUser { UserId = "17ADE1AC-666B-4DAE-83EF-0502728C7CEC", Role = "User" });

            _hasherMock.Setup(u => u.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            //Act
            var result = await _pasteService.GetPasteById(Guid.NewGuid(), password: "123");

            //Assert

            //Combining checks into 1 block that will be executed at the same time
            using AssertionScope _ = new();

            result.getPasteDto.IsPublic.Should().BeFalse();
            result.errorMessage.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetPasteById_WithPrivatePasteAndCorrectPassword_ReturnsPaste()
        {
            //Arrange

            Paste testPaste = new Paste
            {
                IsPublic = false,
                ExpireAt = DateTime.Now.AddMinutes(30)
            };

            _pasteRepositoryMock.Setup(u => u.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(testPaste);

            _mapperMock.Setup(u => u.Map<GetPasteDto>(It.IsAny<Paste>()))
                .Returns(new GetPasteDto { UserId = "123" });

            _amazonS3Mock.Setup(u => u.GetFile(It.IsAny<string>()))
                .ReturnsAsync("PasteBody");

            _userRepositoryMock.Setup(u => u.GetById(It.IsAny<string>()))
                .ReturnsAsync(new ApiUser { UserId = "17ADE1AC-666B-4DAE-83EF-0502728C7CEC", Role = "User" });

            _hasherMock.Setup(u => u.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            //Act
            var result = await _pasteService.GetPasteById(Guid.NewGuid(), password: "123");

            //Assert

            result.getPasteDto.Should().NotBeNull();

        }       
    }
}
