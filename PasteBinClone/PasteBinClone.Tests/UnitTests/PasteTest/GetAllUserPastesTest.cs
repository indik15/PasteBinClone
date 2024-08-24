using FluentAssertions;
using Moq;
using PasteBinClone.Application.Dto;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Tests.UnitTests.PasteTest
{
    public class GetAllUserPastesTest : PasteTestBase
    {
        [Fact]
        public async Task GetAllUserPastes_SuccessResult_ReturnsCollectionOfPastes()
        {
            //Arrange
            int pageNumber = 1;
            int totalPaste = PasteList.Count();

            _pasteRepositoryMock.Setup(u => u.GetAllUserPastes(UserId, pageNumber))
                .ReturnsAsync((PasteList, totalPaste));

            _mapperMock.Setup(u => u.Map<IEnumerable<HomePasteDto>>(It.IsAny<IEnumerable<Paste>>()))
                .Returns(HomePasteDtoList);

            //Act
            var result = await _pasteService.GetAllUserPastes(UserId, pageNumber);

            //Assert
            result.pastes.Should().NotBeNull();
            result.totalPages.Should().NotBe(0);
        }

        [Fact]
        public async Task GetAllUserPastes_WithNullResultFromPasteRepository_ReturnsEmptyCollectionAndZero()
        {
            //Arrange
            int pageNumber = 1;
            int totalPaste = PasteList.Count();

            _pasteRepositoryMock.Setup(u => u.GetAllUserPastes(UserId, pageNumber))
                .ReturnsAsync((null, 0));

            _mapperMock.Setup(u => u.Map<IEnumerable<HomePasteDto>>(It.IsAny<IEnumerable<Paste>>()))
                .Returns(HomePasteDtoList);

            //Act
            var result = await _pasteService.GetAllUserPastes(UserId, pageNumber);

            //Assert
            result.pastes.Should().NotBeNull();
            result.pastes.Should().BeEmpty();
            result.totalPages.Should().Be(0);
        }
    }
}
