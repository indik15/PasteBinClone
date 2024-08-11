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
    public class UpdatePasteTest : PasteTestBase
    {

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
    }
}
