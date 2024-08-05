using Microsoft.EntityFrameworkCore;
using PasteBinClone.Domain.Models;
using PasteBinClone.Persistence.Data;
using PasteBinClone.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Tests.IntegrationTests.LanguageRepositoryTest
{
    public class LanguageRepositoryIntegrationTest : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly LanguageRepository _languageRepository;
        public LanguageRepositoryIntegrationTest()
        {
            _dbContext = AppDbContextInMemory.GetContextInMemory();
            _languageRepository = new LanguageRepository(_dbContext);
        }

        #region Get
        [Fact]
        public async Task Get_ResultSuccess_ReturnsCollectionOfLanguage()
        {
            //Arrange

            //Create a test objects and add them to the database
            _dbContext.AddRange(
                new Language { Id = 1, LanguageName = "Test1" },
                new Language { Id = 2, LanguageName = "Test2" },
                new Language { Id = 3, LanguageName = "Test3" });

            _dbContext.SaveChanges();


            //Act
            var result = await _languageRepository.Get();

            //Assert

            //Verify that the result is not null
            Assert.NotNull(result);

            //Verify that the result is of type List<Language>
            Assert.IsType<List<Language>>(result);
        }

        [Fact]
        public async Task Get_WithEmptyResultFromDb_ReturnsZero()
        {
            //Arrange

            //Act
            var result = await _languageRepository.Get();

            //Assert

            //Verify that the number of result objects is zero
            Assert.Empty(result);
        }
        #endregion

        #region GetById

        [Fact]
        public async Task GetById_ResultSuccess_ReturnsLanguage()
        {
            //Arrange

            //Create a test object
            Language languageTest = new Language { Id = 1, LanguageName = "Test1" };
            int testId = 1;

            _dbContext.Add(languageTest);
            _dbContext.SaveChanges();

            //Act
            var result = await _languageRepository.GetById(testId);

            //Assert

            //Verify that the result is not null
            Assert.NotNull(result);

            //Verify that the result Id equals languageTest Id
            Assert.Equal(languageTest.Id, result.Id);
        }

        [Fact]
        public async Task GetById_WithEmptyResultFromDb_ReturnsNull()
        {
            //Arrange
            int testId = 1;

            //Act
            var result = await _languageRepository.GetById(testId);

            //Assert

            //Verify that the result is null since the database was empty
            Assert.Null(result);
        }

        [Fact]
        public async Task GetById_WithIncorrectId_ReturnsNull()
        {
            //Arrange

            //Create a test object
            Language languageTest = new Language { Id = 1, LanguageName = "Test1" };
            int testId = 0;

            _dbContext.Add(languageTest);
            _dbContext.SaveChanges();

            //Act
            var result = await _languageRepository.GetById(testId);

            //Assert

            //Verify that the result is null since the Id doesn't exist
            Assert.Null(result);
        }
        #endregion

        #region Create

        [Fact]
        public async Task Create_ResultSuccess_ReturnsTrue()
        {
            //Arrange
            Language languageTest = new Language { Id = 1, LanguageName = "Test1" };

            //Act

            //Call the method with the test Language
            var isSuccess = await _languageRepository.Create(languageTest);

            //Verify that the Create method was called and the object was added to the database
            var result = await _dbContext.Languages.FirstOrDefaultAsync(u => u.Id == languageTest.Id);

            //Assert

            //Verify that only one object was added to the database
            Assert.Single(_dbContext.Languages);

            //Verify that the result is not null
            Assert.NotNull(result);

            Assert.True(isSuccess);
        }

        [Fact]
        public async Task Create_WithNullInputObject_ReturnsFalse()
        {
            //Arrange

            //Create a test object
            Language languageTest = null;

            //Act

            //Call the method with the empty test Language
            var result = await _languageRepository.Create(languageTest);

            //Assert
            Assert.False(result);
        }
        #endregion

        #region Update

        [Fact]
        public async Task Update_ResultSuccess_ReturnsTrue()
        {
            //Arrange

            //Create a test object
            Language languageTest = new Language { Id = 1, LanguageName = "Test7" };

            _dbContext.Add(languageTest);
            _dbContext.SaveChanges();

            //Act

            var result = await _languageRepository.Update(new Language { Id = 1, LanguageName = "Test1" });
            var updatedLanguage = await _dbContext.Languages.FirstOrDefaultAsync(u => u.Id == 1);

            //Assert

            //Verify that the result is true
            Assert.True(result);

            //Verify that the object is actually updated
            Assert.Equal("Test1", updatedLanguage.LanguageName);
        }

        [Fact]
        public async Task Update_WithNullInputObject_ReturnsFalse()
        {
            //Arrange

            //Create a test object
            Language languageTest = null;

            //Act
            var result = await _languageRepository.Update(languageTest);

            //Assert

            //Verify that the result is false
            Assert.False(result);
        }

        [Fact]
        public async Task Update_NonExistingObject_ReturnsFalse()
        {
            //Arrange

            //Act
            var result = await _languageRepository.Update(new Language { Id = 1, LanguageName = "Test1" });

            //Assert

            //Verify that the result false
            Assert.False(result);
        }
        #endregion

        #region Delete

        [Fact]
        public async Task Delete_ResultSuccess_ReturnsTrue()
        {
            //Arrange

            //Create a test object
            Language languageTest = new Language { Id = 1, LanguageName = "Test1" };
            int testId = 1;

            _dbContext.Add(languageTest);
            _dbContext.SaveChanges();

            //Act
            var result = await _languageRepository.Delete(testId);

            //Assert

            //Verify that the result is true
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_NonExistingObject_ReturnsFalse()
        {
            //Arrange

            //Act
            var result = await _languageRepository.Delete(1);

            //Assert

            //Verify that the result is false
            Assert.False(result);

        }
        #endregion

        public void Dispose()
        {
            AppDbContextInMemory.Destroy(_dbContext);
        }
    }
}
