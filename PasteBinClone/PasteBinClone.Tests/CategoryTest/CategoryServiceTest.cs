﻿using AutoMapper;
using Moq;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.Services;
using PasteBinClone.Domain.Models;

namespace PasteBinClone.Tests.CategoryTest
{
    public class CategoryServiceTest
    {
        private readonly Mock<IBaseRepository<Category>> _baseRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CategoryServiceTest()
        {
            _baseRepositoryMock = new Mock<IBaseRepository<Category>>();
            _mapperMock = new Mock<IMapper>();
        }

        #region GetAllCategoryTests
        [Fact]
        public async Task GetAllCategory_Result_Success()
        {
            //Arrange
            var categoryList = CreateTestCategories();

            _baseRepositoryMock.Setup(u => u.Get())
                .ReturnsAsync(categoryList);

            var categoryDtoList = CreateTestCategoryDtos();

            _mapperMock.Setup(m => m.Map<IEnumerable<CategoryDto>>(categoryList))
                .Returns(categoryDtoList);

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var categorySeviceResult = await categoryService.GetAllCategory();

            //Assert
            Assert.Equal(categoryDtoList, categorySeviceResult);
        }

        [Fact]
        public async Task GetAllCategory_With_Empty_Result()
        {
            //Arrange
            var categoryList = new List<Category>();

            _baseRepositoryMock.Setup(u => u.Get())
                .ReturnsAsync(categoryList);

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var categoryServiceResult = await categoryService.GetAllCategory();

            //Assert
            Assert.Empty(categoryServiceResult);
        }

        [Fact]
        public async Task GetAllCategory_With_Null_Result_After_Repository_Request()
        {
            //Arrange
            IEnumerable<Category> categoryList = null;

            _baseRepositoryMock.Setup(u => u.Get())
                .ReturnsAsync(categoryList);

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act 
            var categoryServiceResult = await categoryService.GetAllCategory();

            //Assert
            Assert.Null(categoryServiceResult);
        }
        #endregion

        #region GetCategoryById

        [Fact]
        public async Task GetCategoryById_Result_Success()
        {
            //Arrange
            var testCategory = new Category { Id = 1, CategoryName = "Test1" };
            int testUserId = 1;

            _baseRepositoryMock.Setup(u => u.GetById(testUserId))
                .ReturnsAsync(testCategory);

            _mapperMock.Setup(m => m.Map<CategoryDto>(testCategory))
                .Returns(new CategoryDto
                {
                    Id = 1,
                    CategoryName = "Test1"
                });

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var categoryServiceResult = await categoryService.GetCategoryByID(testUserId);

            //Assert
            Assert.IsType<CategoryDto>(categoryServiceResult);
            Assert.Equal(testCategory.Id, categoryServiceResult.Id);
        }

        [Fact]
        public async Task GetCategoryById_NotFound()
        {
            //Arrange
            Category categoryTest = null;
            int testUserId = 1;

            _baseRepositoryMock.Setup(u => u.GetById(testUserId))
                .ReturnsAsync(categoryTest);

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var categoryServiceResult = await categoryService.GetCategoryByID(testUserId);

            //Assert
            Assert.Null(categoryServiceResult);

        }
        #endregion

        private IEnumerable<Category> CreateTestCategories()
        {
            return new List<Category>
            {
                new Category{Id = 1, CategoryName = "Test1"},
                new Category{Id = 2, CategoryName = "Test2"},
                new Category{Id = 3, CategoryName = "Test3"},

            };
        }

        private IEnumerable<CategoryDto> CreateTestCategoryDtos()
        {
            return new List<CategoryDto>
            {
                new CategoryDto{Id = 1, CategoryName = "Test1"},
                new CategoryDto{Id = 2, CategoryName = "Test2"},
                new CategoryDto{Id = 3, CategoryName = "Test3"},

            };
        }
    }
}
