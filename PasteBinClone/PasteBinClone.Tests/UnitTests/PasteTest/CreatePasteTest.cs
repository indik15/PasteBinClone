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
    public class CreatePasteTest : PasteTestBase
    {
        #region CreatePublicPasteTests
        [Fact]
        public async Task CreatePublicPaste_SuccessResult_ReturnsPasteID()
        {
            //Arrange
            _amazonS3Mock.Setup(u => u.UploadFile(PublicPasteDto.Body))
                .ReturnsAsync((true, Guid.NewGuid().ToString()));

            _pasteRepositoryMock.Setup(u => u.Create(It.IsAny<Paste>()))
                .ReturnsAsync(true);

            //Act
            var result = await _pasteService.CreatePaste(PublicPasteDto);

            //Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreatePublicPaste_WithFalseResultFromPasteRepository_ReturnsEmptyGuid()
        {
            //Arrange
            _amazonS3Mock.Setup(u => u.UploadFile(PublicPasteDto.Body))
                .ReturnsAsync((true, Guid.NewGuid().ToString()));

            _pasteRepositoryMock.Setup(u => u.Create(It.IsAny<Paste>()))
                .ReturnsAsync(false);

            //Act
            var result = await _pasteService.CreatePaste(PublicPasteDto);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task CreatePublicPaste_WithFalseResultFromAmazonS3_ReturnsEmptyGuid()
        {
            //Arrange
            _amazonS3Mock.Setup(u => u.UploadFile(PublicPasteDto.Body))
                .ReturnsAsync((false, Guid.NewGuid().ToString()));

            _pasteRepositoryMock.Setup(u => u.Create(It.IsAny<Paste>()))
                .ReturnsAsync(true);

            //Act
            var result = await _pasteService.CreatePaste(PublicPasteDto);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task CreatePublicPaste_WithNullResultInPasteIdFromAmazonS3_ReturnsEmptyGuid()
        {
            //Arrange
            _amazonS3Mock.Setup(u => u.UploadFile(PublicPasteDto.Body))
                .ReturnsAsync((true, null));

            _pasteRepositoryMock.Setup(u => u.Create(It.IsAny<Paste>()))
                .ReturnsAsync(true);

            //Act
            var result = await _pasteService.CreatePaste(PublicPasteDto);

            //Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task CreatePublicPaste_WithNullResultInPasteId_And_WithFalseResultFromAmazonS3_ReturnsEmptyGuid()
        {
            //Arrange
            _amazonS3Mock.Setup(u => u.UploadFile(PublicPasteDto.Body))
                .ReturnsAsync((false, null));

            _pasteRepositoryMock.Setup(u => u.Create(It.IsAny<Paste>()))
                .ReturnsAsync(true);

            //Act
            var result = await _pasteService.CreatePaste(PublicPasteDto);

            //Assert
            result.Should().BeEmpty();
        }
        #endregion

        #region CreatePrivatePasteTests

        [Fact]
        public async Task CreatePrivatePaste_ResultSuccess_ReturnsPasteID()
        {
            //Arrange
            _amazonS3Mock.Setup(u => u.UploadFile(PrivatePasteDto.Body))
                .ReturnsAsync((true, Guid.NewGuid().ToString()));

            _pasteRepositoryMock.Setup(u => u.Create(It.IsAny<Paste>()))
                .ReturnsAsync(true);

            //Act
            var result = await _pasteService.CreatePaste(PrivatePasteDto);

            //Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreatePrivatePaste_WithEmptyPassword_ReturnsEmptyGuid()
        {
            //Arrange
            _amazonS3Mock.Setup(u => u.UploadFile(PrivatePasteDtoNullPassword.Body))
                .ReturnsAsync((true, Guid.NewGuid().ToString()));

            _pasteRepositoryMock.Setup(u => u.Create(It.IsAny<Paste>()))
                .ReturnsAsync(true);

            //Act
            var result = await _pasteService.CreatePaste(PrivatePasteDtoNullPassword);

            //Assert
            result.Should().BeEmpty();
        }
        #endregion
    }
}
