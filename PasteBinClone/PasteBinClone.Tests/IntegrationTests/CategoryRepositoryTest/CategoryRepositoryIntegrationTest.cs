using Microsoft.EntityFrameworkCore;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using PasteBinClone.Persistence.Data;
using PasteBinClone.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Tests.IntegrationTests.CategoryRepositoryTest
{
    public class CategoryRepositoryIntegrationTest : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly CategoryRepository _categoryRepository;
        public CategoryRepositoryIntegrationTest()
        {
            _dbContext = AppDbContextInMemory.GetContextInMemory();
            _categoryRepository = new CategoryRepository(_dbContext);
        }

        #region Create

        [Fact]
        public async Task Create_ResultSuccess_ReturnsTrue()
        {
            //Arrange

            //Create a test object
            Category categoryTest = new Category { Id = 1, CategoryName = "Test1" };

            //Act

            //Call the method with the test Category
            var isSuccess = await _categoryRepository.Create(categoryTest);

            //Verify that the Create method was called and the object was added to the database
            var result = await _dbContext.Categories.FirstOrDefaultAsync(u => u.Id == categoryTest.Id);

            //Assert

            //Verify that only one object was added to the database
            Assert.Single(_dbContext.Categories);

            //Verify that the result is not null
            Assert.NotNull(result);
            Assert.True(isSuccess);
        }

        [Fact]
        public async Task Create_WithNullInputObject_ReturnsFalse()
        {
            //Arrange

            //Create a test object
            Category categoryTest = null;

            //Act

            //Call the method with the empty test Category
            var result = await _categoryRepository.Create(categoryTest);

            //Assert
            Assert.False(result);
        }
        #endregion

        #region GetById

        [Fact]
        public async Task GetById_ResultSuccess_ReturnsCategory()
        {
            //Arrange

            //Create a test object
            Category categoryTest = new Category { Id = 1, CategoryName = "Test1" };
            int testId = 1;

            //Add the test object to the database for testing the method
            _dbContext.Add(categoryTest);
            _dbContext.SaveChanges();

            //Act
            var result = await _categoryRepository.GetById(testId);

            //Assert

            //Verify that the result is not null
            Assert.NotNull(result);

            //Verify that the result Id equals categoryTest Id
            Assert.Equal(categoryTest.Id, result.Id);
        }

        [Fact]
        public async Task GetById_WithIncorrectId_ReturnsNull()
        {
            //Arrange

            //Create a test object
            Category categoryTest = new Category { Id = 1, CategoryName = "Test1" };
            int testId = 0;

            //Add the test object to the database for testing the method
            _dbContext.Add(categoryTest);
            _dbContext.SaveChanges();

            //Act
            var result = await _categoryRepository.GetById(testId);

            //Assert

            //Verify that the result is null since the Id doesn't exist
            Assert.Null(result);
        }

        [Fact]
        public async Task GetById_WithEmptyResultFromDb_ReturnsNull()
        {
            //Arrange

            int testId = 1;

            //Act
            var result = await _categoryRepository.GetById(testId);

            //Assert

            //Verify that the result is null since the database was empty
            Assert.Null(result);
        }
        #endregion

        #region Get

        [Fact]
        public async Task Get_ResultSuccess_ReturnsCollectionOfCategory()
        {
            //Arrange

            //Create a test objects and add them to the database
            _dbContext.AddRange(
                    new Category() { Id = 1, CategoryName = "Test1" },
                    new Category() { Id = 2, CategoryName = "Test2" },
                    new Category() { Id = 3, CategoryName = "Test3" }

                );
            _dbContext.SaveChanges();

            //Act
            var result = await _categoryRepository.Get();

            //Assert

            //Verify that the result is not null
            Assert.NotNull(result);

            //Verify that the result is of type List<Category>
            Assert.IsType<List<Category>>(result);
        }

        [Fact]
        public async Task Get_WithEmptyResultFromDb_ReturnsZero()
        {
            //Arrange

            //Act
            var result = await _categoryRepository.Get();

            //Assert

            //Verify that the number of result objects is zero
            Assert.Empty(result);
        }
        #endregion

        #region Update

        [Fact]
        public async Task Update_ResultSuccess_ReturnsTrue()
        {
            //Arrange

            //Create a test object
            Category categoryTest = new Category { Id = 1, CategoryName = "Test5" };

            //Add the test object to the database
            _dbContext.Add(categoryTest);
            _dbContext.SaveChanges();

            //Act
            var result = await _categoryRepository.Update(new Category { Id = 1, CategoryName = "Test1" });
            var updatedCategory = await _dbContext.Categories.FirstOrDefaultAsync(u => u.Id == 1);

            //Assert

            //Verify that the result is true
            Assert.True(result);

            //Verify that the object is actually updated
            Assert.Equal("Test1", updatedCategory.CategoryName);
        }

        [Fact]
        public async Task Update_WithNullInputObject_ReturnsFalse()
        {
            //Arrange

            //Create a test object
            Category categoryTest = null;

            //Act
            var result = await _categoryRepository.Update(categoryTest);

            //Assert

            //Verify that the result is false
            Assert.False(result);
        }

        [Fact]
        public async Task Update_NonExistingObject_ReturnsFalse()
        {
            //Arrange

            //Act
            var result = await _categoryRepository.Update(new Category { Id = 1, CategoryName = "Test1" });

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
            Category categoryTest = new Category { Id = 1, CategoryName = "Test5" };

            //Add the test object to the database
            _dbContext.Add(categoryTest);
            _dbContext.SaveChanges();


            //Act
            var result = await _categoryRepository.Delete(categoryTest.Id);

            //Assert

            //Verify that the result is true
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_NonExistingObject_ReturnsFalse()
        {
            //Arrange

            //Act
            var result = await _categoryRepository.Delete(1);

            //Assert

            //Verify that the result is false
            Assert.False(result);

        }
        #endregion

        private IEnumerable<Category> GetAllCategory()
        {
            return new List<Category>
            {
                new Category { Id = 1, CategoryName = "TestName1" },
                new Category { Id = 2, CategoryName = "TestName2" },
                new Category { Id = 3, CategoryName = "TestName3" },
            };
        }

        public void Dispose()
        {
            AppDbContextInMemory.Destroy(_dbContext);
        }
    }
}
