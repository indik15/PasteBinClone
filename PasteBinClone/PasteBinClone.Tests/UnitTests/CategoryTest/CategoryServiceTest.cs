using AutoMapper;
using Moq;
using PasteBinClone.Application.Dto;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Application.Services;
using PasteBinClone.Domain.Models;

namespace PasteBinClone.Tests.UnitTests.CategoryTest
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
        public async Task GetAllCategory_SuccessResult_ReturnsCollectionOfCategory()
        {
            //Arrange

            //Settings for a mock methods that will return a list of objects
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

            //Ensure that the result is not null
            Assert.NotNull(categorySeviceResult);
            //Check the expected result count
            Assert.Equal(categoryDtoList.Count(), categorySeviceResult.Count());
        }

        [Fact]
        public async Task GetAllCategory_WithEmptyResultFromRepository_ReturnsEmptyCollection()
        {
            //Arrange

            //Settings for a mock methods that will return a list of objects
            var categoryList = new List<Category>();

            _baseRepositoryMock.Setup(u => u.Get())
                .ReturnsAsync(categoryList);

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var categoryServiceResult = await categoryService.GetAllCategory();

            //Assert

            //Ensure that the object count is zero 
            Assert.Empty(categoryServiceResult);
        }

        [Fact]
        public async Task GetAllCategory_WithNullResultFromRepository_ReturnsNull()
        {
            //Arrange

            //Settings for a mock methods that will return a list of objects
            IEnumerable<Category> categoryList = null;

            _baseRepositoryMock.Setup(u => u.Get())
                .ReturnsAsync(categoryList);

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act 
            var categoryServiceResult = await categoryService.GetAllCategory();

            //Assert

            //Ensure that the result is null 
            Assert.Null(categoryServiceResult);
        }
        #endregion

        #region GetCategoryByIdTests

        [Fact]
        public async Task GetCategoryById_SuccessResult_ReturnsCategory()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            var testCategory = new Category { Id = 1, CategoryName = "Test1" };
            int testId = 1;

            _baseRepositoryMock.Setup(u => u.GetById(testId))
                .ReturnsAsync(testCategory);

            _mapperMock.Setup(m => m.Map<CategoryDto>(testCategory))
                .Returns(new CategoryDto
                {
                    Id = 1,
                    CategoryName = "Test1"
                });

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var categoryServiceResult = await categoryService.GetCategoryByID(testId);

            //Assert

            //Ensure that the result type matches the expected type
            Assert.IsType<CategoryDto>(categoryServiceResult);
            //Ensure that the expected id is equal result id  
            Assert.Equal(testCategory.Id, categoryServiceResult.Id);
        }

        [Fact]
        public async Task GetCategoryById_NullResultFromRepository_ReturnsNull()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            Category categoryTest = null;
            int testId = 1;

            _baseRepositoryMock.Setup(u => u.GetById(testId))
                .ReturnsAsync(categoryTest);

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var categoryServiceResult = await categoryService.GetCategoryByID(testId);

            //Assert

            //Ensure that the result is null
            Assert.Null(categoryServiceResult);

        }
        #endregion      

        #region CreateCategoryTests
        [Fact]
        public async Task CreateCategory_WithFalseResultFromRepository_ReturnsFalse()
        {
            //Arrange

            //Settings for a mock methods that will return an boolean
            var testCategory = new Category { Id = 1, CategoryName = "Test1" };
            var testCategoryDto = new CategoryDto { Id = 1, CategoryName = "Test1" };
            bool testResult = false;

            _baseRepositoryMock.Setup(u => u.Create(testCategory))
                .ReturnsAsync(testResult);

            _mapperMock.Setup(u => u.Map<Category>(testCategoryDto))
                .Returns(testCategory);

            var contentTypeService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.CreateCategory(testCategoryDto);

            //Assert

            //Ensure that the result is false
            Assert.False(result);
        }

        [Fact]
        public async Task CreateCategory_SuccessResult_ReturnsTrue()
        {
            //Arrange

            //Settings for a mock methods that will return an boolean
            var testCategory = new Category { Id = 1, CategoryName = "Test1" };
            var testCategoryDto = new CategoryDto { Id = 1, CategoryName = "Test1" };
            bool testResult = true;

            _baseRepositoryMock.Setup(u => u.Create(testCategory))
                .ReturnsAsync(testResult);

            _mapperMock.Setup(u => u.Map<Category>(testCategoryDto))
                .Returns(testCategory);

            var contentTypeService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.CreateCategory(testCategoryDto);

            //Assert

            //Ensure that the result is true
            Assert.True(result);
        }
        #endregion

        #region UpdateCategoryTests

        [Fact]
        public async Task UpdateCategory_SuccessResult_ReturnsTrue()
        {
            //Arrange

            //Settings for a mock methods that will return an boolean
            CategoryDto categoryDtoTest = new CategoryDto { Id = 1, CategoryName = "Test1" };
            Category categoryTest = new Category { Id = 1, CategoryName = "Test1" };
            bool result = true;

            _baseRepositoryMock.Setup(u => u.Update(categoryTest))
                .ReturnsAsync(result);

            _mapperMock.Setup(m => m.Map<Category>(categoryDtoTest))
                .Returns(categoryTest);

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var categoryServiceResult = await categoryService.UpdateCategory(categoryDtoTest);

            //Assert

            //Ensure that the result is true
            Assert.True(categoryServiceResult);
        }

        [Fact]
        public async Task UpdateCategory_WithFalseResultFromRepository_ReturnsFalse()
        {
            //Arrange

            //Settings for a mock methods that will return an boolean
            CategoryDto categoryDtoTest = new CategoryDto { Id = 1, CategoryName = "Test1" };
            Category categoryTest = new Category { Id = 1, CategoryName = "Test1" };
            bool failResult = false;

            _baseRepositoryMock.Setup(m => m.Update(categoryTest))
                .ReturnsAsync(failResult);
            _mapperMock.Setup(m => m.Map<Category>(categoryDtoTest))
                .Returns(categoryTest);

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var categoryServiceResult = await categoryService.UpdateCategory(categoryDtoTest);

            //Assert

            //Ensure that the result is false
            Assert.False(categoryServiceResult);
        }
        #endregion

        #region DeleteCategoryTests
        [Fact]
        public async Task DeleteCategory_SuccessResult_ReturnsTrue()
        {
            //Arrange

            //Settings for a mock method that will return an boolean
            int testId = 1;
            bool result = true;

            _baseRepositoryMock.Setup(u => u.Delete(testId))
                .ReturnsAsync(result);

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var categoryServiceResult = await categoryService.DeleteCategory(testId);

            //Assert

            //Ensure that the result is true
            Assert.True(categoryServiceResult);
        }

        [Fact]
        public async Task DeleteCategory_WithFalseResultFromRepository_ReturnsFalse()
        {
            //Arrange

            //Settings for a mock method that will return an boolean
            int testId = 1;
            bool result = false;


            _baseRepositoryMock.Setup(m => m.Delete(testId))
                .ReturnsAsync(result);

            var categoryService = new CategoryService(_baseRepositoryMock.Object, _mapperMock.Object);

            //Act
            var categoryServiceResult = await categoryService.DeleteCategory(testId);

            //Assert

            //Ensure that the result is false
            Assert.False(categoryServiceResult);
        }
        #endregion

        //Added methods that returned a test object
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
