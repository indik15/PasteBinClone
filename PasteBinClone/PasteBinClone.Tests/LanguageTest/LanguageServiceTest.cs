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

namespace PasteBinClone.Tests.LanguageTest
{
    public class LanguageServiceTest
    {
        private readonly Mock<IBaseRepository<Language>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public LanguageServiceTest()
        {
            _repositoryMock = new Mock<IBaseRepository<Language>>();
            _mapperMock = new Mock<IMapper>();
        }

        #region GetAllLanguage

        [Fact]
        public async Task GetAllLanguage_SuccessResult_ReturnsCollectionOfLanguage()
        {
            //Arrange

            //Settings for a mock methods that will return a list of objects
            var expectedLanguages = CreateTestLanguage();
            var expectedLanguagesDto = CreateTestLanguageDto();

            _repositoryMock.Setup(u => u.Get())
                .ReturnsAsync(expectedLanguages);

            _mapperMock.Setup(u => u.Map<IEnumerable<LanguageDto>>(expectedLanguages))
                .Returns(expectedLanguagesDto);

            var languageService = new LanguageService(_mapperMock.Object, _repositoryMock.Object);

            //Act
            var result = await languageService.GetAllLanguage();

            //Assert

            //Ensure that the result is not null
            Assert.NotNull(result);
            //Check the expected result count
            Assert.Equal(expectedLanguagesDto.Count(), result.Count());
        }

        [Fact]
        public async Task GetAllLanguage_WithEmptyResultFromRepository_ReturnsEmptyCollection()
        {
            //Arrange

            //Settings for a mock methods that will return a list of objects
            var expectedLanguages = new List<Language>();
            var expectedLanguagesDto = new List<LanguageDto>();

            _repositoryMock.Setup(u => u.Get())
                .ReturnsAsync(expectedLanguages);

            _mapperMock.Setup(u => u.Map<IEnumerable<LanguageDto>>(expectedLanguages))
                .Returns(expectedLanguagesDto);

            var languageService = new LanguageService(_mapperMock.Object, _repositoryMock.Object);

            //Act
            var result = await languageService.GetAllLanguage();

            //Assert

            //Ensure that the object count is zero 
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllLanguage_WithNullResultFromRepository_ReturnsNull()
        {
            //Arrange

            //Settings for a mock methods that will return a list of objects
            IEnumerable<Language> expectedLanguage = null;

            _repositoryMock.Setup(u => u.Get())
                .ReturnsAsync(expectedLanguage);

            var languageService = new LanguageService(_mapperMock.Object, _repositoryMock.Object);

            //Act
            var result = await languageService.GetAllLanguage();

            //Assert

            //Ensure that the result is null 
            Assert.Null(result);
        }
        #endregion

        #region GetLanguageByID

        [Fact]
        public async Task GetLanguageByID_SuccessResult_ReturnsLanguage()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            var expectedLanguage = new Language { Id = 1, LanguageName = "Test1" };
            var expectedLanguageDto = new LanguageDto { Id = 1, LanguageName = "Test1" };
            int testId = 1;

            _repositoryMock.Setup(u => u.GetById(testId))
                .ReturnsAsync(expectedLanguage);

            _mapperMock.Setup(u => u.Map<LanguageDto>(expectedLanguage))
                .Returns(expectedLanguageDto);

            var languageService = new LanguageService(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var result = await languageService.GetLanguageByID(testId);

            //Assert

            //Ensure that the result type matches the expected type
            Assert.IsType<LanguageDto>(result);
            //Ensure that the expected id is equal result id  
            Assert.Equal(expectedLanguage.Id, result.Id);
        }

        [Fact]
        public async Task GetLanguageByID_NullResultFromRepository_ReturnsNull()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            Language expectedLanguage = null;
            int testId = 1;

            _repositoryMock.Setup(u => u.GetById(testId))
                .ReturnsAsync(expectedLanguage);

            var languageService = new LanguageService(_mapperMock.Object, _repositoryMock.Object);

            //Act
            var result = await languageService.GetLanguageByID(testId);

            //Assert

            //Ensure that the result is null
            Assert.Null(result);
        }
        #endregion

        #region CreateLanguage

        [Fact]
        public async Task CreateLanguage_SuccessResult_ReturnsTrue()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            var expectedLanguage = new Language { Id = 1, LanguageName = "Test1" };
            var expectedLanguageDto = new LanguageDto { Id = 1, LanguageName = "Test1" };
            bool testResult = true;

            _repositoryMock.Setup(u => u.Create(expectedLanguage))
                .ReturnsAsync(testResult);

            _mapperMock.Setup(u => u.Map<Language>(expectedLanguageDto))
                .Returns(expectedLanguage);

            var languageService = new LanguageService(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var result = await languageService.CreateLanguage(expectedLanguageDto);

            //Assert

            //Ensure that the result is true
            Assert.True(result);
        }

        [Fact]
        public async Task CreateLanguage_WithFalseResultFromRepository_ReturnsFalse()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            var expectedLanguage = new Language { Id = 1, LanguageName = "Test1" };
            var expectedLanguageDto = new LanguageDto { Id = 1, LanguageName = "Test1" };
            bool testResult = false;

            _repositoryMock.Setup(u => u.Create(expectedLanguage))
                .ReturnsAsync(testResult);

            _mapperMock.Setup(u => u.Map<Language>(expectedLanguageDto))
                .Returns(expectedLanguage);

            var languageService = new LanguageService(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var result = await languageService.CreateLanguage(expectedLanguageDto);

            //Assert

            //Ensure that the result is false
            Assert.False(result);
        }
        #endregion

        #region UpdateLanguage

        [Fact]
        public async Task UpdateLanguage_SuccessResult_ReturnsTrue()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            var expectedLanguage = new Language { Id = 1, LanguageName = "Test1" };
            var expectedLanguageDto = new LanguageDto { Id = 1, LanguageName = "Test1" };
            bool testResult = true;

            _repositoryMock.Setup(u => u.Update(expectedLanguage))
                .ReturnsAsync(testResult);

            _mapperMock.Setup(u => u.Map<Language>(expectedLanguageDto))
                .Returns(expectedLanguage);

            var languageService = new LanguageService(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var result = await languageService.UpdateLanguage(expectedLanguageDto);

            //Assert

            //Ensure that the result is true
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateLanguage_WithFalseResultFromRepository_ReturnsFalse()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            var expectedLanguage = new Language { Id = 1, LanguageName = "Test1" };
            var expectedLanguageDto = new LanguageDto { Id = 1, LanguageName = "Test1" };
            bool testResult = false;

            _repositoryMock.Setup(u => u.Update(expectedLanguage))
                .ReturnsAsync(testResult);

            _mapperMock.Setup(u => u.Map<Language>(expectedLanguageDto))
                .Returns(expectedLanguage);

            var languageService = new LanguageService(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var result = await languageService.UpdateLanguage(expectedLanguageDto);

            //Assert

            //Ensure that the result is false
            Assert.False(result);
        }
        #endregion

        #region DeleteLanguage

        [Fact]
        public async Task DeleteLanguage_SuccessResult_ReturnsTrue()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            int testId = 1;
            bool testResult = true;

            _repositoryMock.Setup(u => u.Delete(testId))
                .ReturnsAsync(testResult);

            var languageService = new LanguageService(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var result = await languageService.DeleteLanguage(testId);

            //Assert

            //Ensure that the result is true
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteLanguage_WithFalseResultFromRepository_ReturnsFalse()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            int testId = 1;
            bool testResult = false;

            _repositoryMock.Setup(u => u.Delete(testId))
                .ReturnsAsync(testResult);

            var languageService = new LanguageService(_mapperMock.Object, _repositoryMock.Object);

            //Act

            var result = await languageService.DeleteLanguage(testId);

            //Assert

            //Ensure that the result is false
            Assert.False(result);
        }
        #endregion


        //Added methods that returned a test object
        private IEnumerable<Language> CreateTestLanguage()
        {
            return new List<Language>
            {
                new Language {Id = 1, LanguageName = "Test1"},
                new Language {Id = 2, LanguageName = "Test2"},
                new Language {Id = 3, LanguageName = "Test3"},

            };
        }

        private IEnumerable<LanguageDto> CreateTestLanguageDto()
        {
            return new List<LanguageDto>
            {
                new LanguageDto {Id = 1, LanguageName = "Test1"},
                new LanguageDto {Id = 2, LanguageName = "Test2"},
                new LanguageDto {Id = 3, LanguageName = "Test3"},

            };
        }
    }
}
