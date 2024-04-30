using Microsoft.EntityFrameworkCore;
using PasteBinClone.Domain.Models;
using PasteBinClone.Persistence.Data;
using PasteBinClone.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Tests.ContentTypeRepositoryTest
{
    public class ContentTypeRepositoryIntegrationTest : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ContentTypeRepository _contentTypeRepository;

        public ContentTypeRepositoryIntegrationTest()
        {
            _dbContext = AppDbContextInMemory.GetContextInMemory();
            _contentTypeRepository = new ContentTypeRepository(_dbContext);
        }

        #region Get
        [Fact]
        public async Task Get_ResultSuccess_ReturnsCollectionOfContentType()
        {
            //Arrange

            //Create a test objects and add them to the database
            _dbContext.AddRange(
                new ContentType { Id = 1, TypeName = "Test1" },
                new ContentType { Id = 2, TypeName = "Test2" },
                new ContentType { Id = 3, TypeName = "Test3" });

            _dbContext.SaveChanges();


            //Act
            var result = await _contentTypeRepository.Get();

            //Assert

            //Verify that the result is not null
            Assert.NotNull(result);

            //Verify that the result is of type List<ContentType>
            Assert.IsType<List<ContentType>>(result);
        }

        [Fact]
        public async Task Get_WithEmptyResultFromDb_ReturnsZero()
        {
            //Arrange

            //Act
            var result = await _contentTypeRepository.Get();

            //Assert

            //Verify that the number of result objects is zero
            Assert.Empty(result);
        }
        #endregion

        #region GetById

        [Fact]
        public async Task GetById_ResultSuccess_ReturnsContentType()
        {
            //Arrange

            //Create a test object
            ContentType contentTypeTest = new ContentType { Id = 1, TypeName = "Test1" };
            int testId = 1;

            _dbContext.Add(contentTypeTest);
            _dbContext.SaveChanges();

            //Act
            var result = await _contentTypeRepository.GetById(testId);

            //Assert

            //Verify that the result is not null
            Assert.NotNull(result);

            //Verify that the result Id equals categoryTest Id
            Assert.Equal(contentTypeTest.Id, result.Id);
        }

        [Fact]
        public async Task GetById_WithEmptyResultFromDb_ReturnsNull()
        {
            //Arrange
            int testId = 1;

            //Act
            var result = await _contentTypeRepository.GetById(testId);

            //Assert

            //Verify that the result is null since the database was empty
            Assert.Null(result);
        }

        [Fact]
        public async Task GetById_WithIncorrectId_ReturnsNull()
        {
            //Arrange

            //Create a test object
            ContentType contentTypeTest = new ContentType { Id = 1, TypeName = "Test1" };
            int testId = 0;

            _dbContext.Add(contentTypeTest);
            _dbContext.SaveChanges();

            //Act
            var result = await _contentTypeRepository.GetById(testId);

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
            ContentType contentTypeTest = new ContentType { Id = 1, TypeName = "Test1" };

            //Act

            //Call the method with the test ContentType
            var isSuccess = await _contentTypeRepository.Create(contentTypeTest);

            //Verify that the Create method was called and the object was added to the database
            var result = await _dbContext.ContentTypes.FirstOrDefaultAsync(u => u.Id == contentTypeTest.Id);

            //Assert

            //Verify that only one object was added to the database
            Assert.Single(_dbContext.ContentTypes);

            //Verify that the result is not null
            Assert.NotNull(result);

            Assert.True(isSuccess);
        }

        [Fact]
        public async Task Create_WithNullInputObject_ReturnsFalse()
        {
            //Arrange

            //Create a test object
            ContentType contentTypeTest = null;

            //Act

            //Call the method with the empty test ContentType
            var result = await _contentTypeRepository.Create(contentTypeTest);

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
            ContentType contentTypeTest = new ContentType { Id = 1, TypeName = "Test7" };

            _dbContext.Add(contentTypeTest);
            _dbContext.SaveChanges();

            //Act

            var result = await _contentTypeRepository.Update(new ContentType { Id = 1, TypeName = "Test1"});
            var updatedContentType = await _dbContext.ContentTypes.FirstOrDefaultAsync(u => u.Id == 1);

            //Assert

            //Verify that the result is true
            Assert.True(result);

            //Verify that the object is actually updated
            Assert.Equal("Test1", updatedContentType.TypeName);
        }

        [Fact]
        public async Task Update_WithNullInputObject_ReturnsFalse()
        {
            //Arrange

            //Create a test object
            ContentType contentTypeTest = null;

            //Act
            var result = await _contentTypeRepository.Update(contentTypeTest);

            //Assert

            //Verify that the result is false
            Assert.False(result);
        }

        [Fact]
        public async Task Update_NonExistingObject_ReturnsFalse()
        {
            //Arrange

            //Act
            var result = await _contentTypeRepository.Update(new ContentType { Id = 1, TypeName = "Test1" });

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
            ContentType contentTypeTest = new ContentType { Id = 1, TypeName = "Test1" };
            int testId = 1;

            _dbContext.Add(contentTypeTest);
            _dbContext.SaveChanges();

            //Act
            var result = await _contentTypeRepository.Delete(testId);

            //Assert

            //Verify that the result is true
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_NonExistingObject_ReturnsFalse()
        {
            //Arrange

            //Act
            var result = await _contentTypeRepository.Delete(1);

            //Assert

            //Verify that the result is false
            Assert.False(result);

        }
        #endregion

        //Added methods that returned a test object
        private IEnumerable<ContentType> CreateTestContentType()
        {
            return new List<ContentType>
            {
                new ContentType {Id = 1, TypeName = "Test1"},
                new ContentType {Id = 2, TypeName = "Test2"},
                new ContentType {Id = 3, TypeName = "Test3"},

            };
        }

        public void Dispose()
        {
            AppDbContextInMemory.Destroy(_dbContext);
        }
    }
}
