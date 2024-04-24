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

namespace PasteBinClone.Tests.CategoryRepositoryTest
{
    public class CategoryRepositoryIntegrationTest : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepositoryIntegrationTest()
        {
            _dbContext = AppDbContextInMemory.GetContextInMemory();
        }

        [Fact]
        public async Task Create_Success()
        {
            //Arrange

            //Create a test object
            Category categoryTest = new Category { Id = 1, CategoryName = "Test1" };

            //Add _dbContext options with an In-Memory database
            var categoryRepository = new CategoryRepository(_dbContext);

            //Act

            //Call the method with the test Category
            await categoryRepository.Create(categoryTest);

            //Verify that the Create method was called and the object was added to the database
            var result = await _dbContext.Categories.FirstOrDefaultAsync(u => u.Id ==  categoryTest.Id);

            //Assert

            //Verify that only one object was added to the database
            Assert.Single(_dbContext.Categories);

            //Verify that the result is not null
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetById_Success()
        {
            //Arrange

            //Create a test object
            Category categoryTest = new Category { Id = 1, CategoryName = "Test1" };
            int testId = 1;

            //Add _dbContext options with an In-Memory database
            var categoryRepository = new CategoryRepository(_dbContext);

            //Add the test object to the database for testing the method
            _dbContext.Add(categoryTest);
            _dbContext.SaveChanges();

            //Act

            //Call the method with the test Id
            var result = await categoryRepository.GetById(testId);

            //Assert

            //Verify that the result is not null
            Assert.NotNull(result);

            //Verify that the result Id equals categoryTest Id
            Assert.Equal(categoryTest.Id, result.Id);
        }

        [Fact]
        public async Task GetById_With_Incorrect_Id()
        {
            //Arrange

            //Create a test object
            Category categoryTest = new Category { Id = 1, CategoryName = "Test1" };

            // Choosing an incorrect test Id
            int testId = 0;

            //Add _dbContext options with an In-Memory database
            var categoryRepository = new CategoryRepository(_dbContext);

            //Add the test object to the database for testing the method
            _dbContext.Add(categoryTest);
            _dbContext.SaveChanges();

            //Act

            //Call the method with the incorrect test Id
            var result = await categoryRepository.GetById(testId);

            //Assert

            //Verify that the result is null since the Id doesn't exist
            Assert.Null(result);
        }

        [Fact]
        public async Task GetById_With_Empty_Db()
        {
            //Arrange

            //Add _dbContext options with an In-Memory database
            var categoryRepository = new CategoryRepository(_dbContext);
            int testId = 1;

            //Act

            //Call the method with the empty database
            var result = await categoryRepository.GetById(testId);

            //Assert

            //Verify that the result is null since the database was empty
            Assert.Null(result);
        }

        [Fact]
        public async Task Get_Success()
        {
            //Arrange

            //Create a test objects and add them to the database
            _dbContext.AddRange(
                    new Category() { Id = 1, CategoryName = "Test1" },
                    new Category() { Id = 2, CategoryName = "Test2" },
                    new Category() { Id = 3, CategoryName = "Test3" }

                );
            _dbContext.SaveChanges();

            //Add _dbContext options with an In-Memory database
            var categoryRepository = new CategoryRepository(_dbContext);

            //Act

            //Call the method to get all objects
            var result = await categoryRepository.Get();

            //Assert

            //Verify that the result is not null
            Assert.NotNull(result);

            //Verify that the result is of type List<Category>
            Assert.IsType<List<Category>>(result);
        }

        [Fact]
        public async Task Get_With_Empty_Db()
        {
            //Arrange

            //Add _dbContext options with an In-Memory database
            var categoryRepository = new CategoryRepository(_dbContext);

            //Act

            //Call the method to get all objects
            var result = await categoryRepository.Get();

            //Assert

            //Verify that the number of result objects is zero
            Assert.True(result.Count() == 0);
        }

        [Fact]
        public async Task Update_Success()
        {
            //Arrange

            //Create a test object
            Category categoryTest = new Category { Id = 1, CategoryName = "Test5" };

            //Add the test object to the database
            _dbContext.Add(categoryTest);
            _dbContext.SaveChanges();

            //Add _dbContext options with an In-Memory database
            var categoryRepository = new CategoryRepository(_dbContext);

            //Act

            //Call the method to update object
            var result = await categoryRepository.Update(new Category { Id = 1, CategoryName = "Test1" });
            var updatedCategory = await _dbContext.Categories.FirstOrDefaultAsync(u => u.Id == 1);

            //Assert

            //Verify that the result is true
            Assert.True(result);

            //Verify that the object is actually updated
            Assert.Equal("Test1", updatedCategory.CategoryName);
        }

        [Fact]
        public async Task Update_NonExisting_Category()
        {
            //Arrange

            //Add _dbContext options with an In-Memory database
            var categoryRepository = new CategoryRepository(_dbContext);

            //Act

            //Call the method to update object
            var result = await categoryRepository.Update(new Category { Id = 1, CategoryName = "Test1" });

            //Assert

            //Verify that the result false
            Assert.False(result);
        }

        [Fact]
        public async Task Delete_Success()
        {
            //Arrange

            //Create a test object
            Category categoryTest = new Category { Id = 1, CategoryName = "Test5" };

            //Add the test object to the database
            _dbContext.Add(categoryTest);
            _dbContext.SaveChanges();

            //Add _dbContext options with an In-Memory database
            var categoryRepository = new CategoryRepository(_dbContext);

            //Act

            //Call the method to delete object
            var result = await categoryRepository.Delete(categoryTest.Id);

            //Assert

            //Verify that the result is true
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_NonExisting_Category()
        {
            //Arrange

            //Add _dbContext options with an In-Memory database
            var categoryRepository = new CategoryRepository(_dbContext);

            //Act

            //Call the method to delete object
            var result = await categoryRepository.Delete(1);

            //Assert

            //Verify that the result is false
            Assert.False(result);

        }

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
