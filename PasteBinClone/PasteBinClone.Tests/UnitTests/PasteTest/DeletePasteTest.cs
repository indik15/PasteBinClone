using AutoMapper;
using FluentAssertions;
using Moq;
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
    public class DeletePasteTest : PasteTestBase
    {       
        [Fact]
        public async Task DeletePaste_SuccessResult_ReturnsTrue()
        {
            //Arrange
            Guid pasteId = Guid.NewGuid();

            _pasteRepositoryMock.Setup(u => u.GetById(pasteId))
                .ReturnsAsync(PublicPaste);

            _pasteRepositoryMock.Setup(u => u.Delete(pasteId))
                .ReturnsAsync(true);

            _amazonS3Mock.Setup(u => u.DeleteFile(PublicPaste.BodyUrl))
                .ReturnsAsync(true);

            //Act
            var result = await _pasteService.DeletePaste(pasteId);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeletePaste_WithNullResultFromGetByIdInRepository_ReturnsFalse()
        {
            //Arrange
            Guid pasteId = Guid.NewGuid();

            _pasteRepositoryMock.Setup(u => u.GetById(pasteId))
                .ReturnsAsync((Paste)null);

            _pasteRepositoryMock.Setup(u => u.Delete(pasteId))
                .ReturnsAsync(true);

            _amazonS3Mock.Setup(u => u.DeleteFile(PublicPaste.BodyUrl))
                .ReturnsAsync(true);

            //Act
            var result = await _pasteService.DeletePaste(pasteId);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeletePaste_WithFalseResultDeleteInRepository_ReturnsFalse()
        {
            //Arrange
            Guid pasteId = Guid.NewGuid();

            _pasteRepositoryMock.Setup(u => u.GetById(pasteId))
                .ReturnsAsync(PublicPaste);

            _pasteRepositoryMock.Setup(u => u.Delete(pasteId))
                .ReturnsAsync(false);

            _amazonS3Mock.Setup(u => u.DeleteFile(PublicPaste.BodyUrl))
                .ReturnsAsync(true);

            //Act
            var result = await _pasteService.DeletePaste(pasteId);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeletePaste_WithFalseResultInAmazonS3_ReturnsFalse()
        {
            //Arrange
            Guid pasteId = Guid.NewGuid();

            _pasteRepositoryMock.Setup(u => u.GetById(pasteId))
                .ReturnsAsync(PublicPaste);

            _pasteRepositoryMock.Setup(u => u.Delete(pasteId))
                .ReturnsAsync(true);

            _amazonS3Mock.Setup(u => u.DeleteFile(PublicPaste.BodyUrl))
                .ReturnsAsync(false);

            //Act
            var result = await _pasteService.DeletePaste(pasteId);

            //Assert
            result.Should().BeFalse();
        }
    }
}
