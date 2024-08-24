using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.ViewModels;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Tests.UnitTests.PasteTest
{
    public class GetFiveUserPastesTest : PasteTestBase
    {
        [Fact]
        public async Task GetFiveUserPastes_SuccessResult_ReturnsCollectionOfPastes()
        {
            //Arrange
            _pasteRepositoryMock.Setup(u => u.GetFiveUserPastes(UserId))
                .ReturnsAsync(PasteList);

            _mapperMock.Setup(u => u.Map<IEnumerable<HomePasteDto>>(It.IsAny<IEnumerable<Paste>>()))
                .Returns(HomePasteDtoList);

            //Act
            var result = await _pasteService.GetFiveUserPastes(UserId);

            //Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetFiveUserPastes_WithNullResultFromPasteRepository_ReturnsEmptyCollection()
        {
            //Arrange
            _pasteRepositoryMock.Setup(u => u.GetFiveUserPastes(UserId))
                .ReturnsAsync((IEnumerable<Paste>)null);

            _mapperMock.Setup(u => u.Map<IEnumerable<HomePasteDto>>(It.IsAny<IEnumerable<Paste>>()))
                .Returns(HomePasteDtoList);

            //Act
            var result = await _pasteService.GetFiveUserPastes(UserId);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeNull();
        }
    }
}
