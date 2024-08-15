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
    public class GetAllPasteTest : PasteTestBase
    {
        [Fact]
        public async Task GetAllPaste_SuccessResult_ReturnsCollectionOfPastes()
        {
            //Arrange
            _pasteRepositoryMock.Setup(u => u.Get(It.IsAny<HomePasteRequestDto>()))
                .ReturnsAsync((PasteList, 15));

            _mapperMock.Setup(u => u.Map<IEnumerable<HomePasteDto>>(It.IsAny<IEnumerable<Paste>>()))
                .Returns(HomePasteDtoList);

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

            _mapperMock.Setup(u => u.Map<IEnumerable<HomePasteDto>>(It.IsAny<IEnumerable<Paste>>()))
                .Returns(HomePasteDtoList);

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

            _mapperMock.Setup(u => u.Map<IEnumerable<HomePasteDto>>(It.IsAny<IEnumerable<Paste>>()))
                .Returns(HomePasteDtoList);

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

            _mapperMock.Setup(u => u.Map<IEnumerable<HomePasteDto>>(It.IsAny<IEnumerable<Paste>>()))
                .Returns(HomePasteDtoList);

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

            _mapperMock.Setup(u => u.Map<IEnumerable<HomePasteDto>>(It.IsAny<IEnumerable<Paste>>()))
                .Returns(HomePasteDtoList);

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
    }
}
