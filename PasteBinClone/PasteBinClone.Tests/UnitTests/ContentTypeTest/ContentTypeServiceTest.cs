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

namespace PasteBinClone.Tests.UnitTests.ContentTypeTest
{
    public class ContentTypeServiceTest
    {
        private readonly Mock<IBaseRepository<ContentType>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public ContentTypeServiceTest()
        {
            _repositoryMock = new Mock<IBaseRepository<ContentType>>();
            _mapperMock = new Mock<IMapper>();
        }

        #region GetAllContentTypeTests
        [Fact]
        public async Task GetAllContentType_SuccessResult_ReturnsCollectionOfContentType()
        {
            //Arrange

            //Settings for a mock methods that will return a list of objects
            var expectedContentTypes = CreateTestContentType();
            var expectedContentTypesDto = CreateTestContentTypeDto();

            _repositoryMock.Setup(u => u.Get())
                .ReturnsAsync(expectedContentTypes);

            _mapperMock.Setup(u => u.Map<IEnumerable<ContentTypeDto>>(expectedContentTypes))
                .Returns(expectedContentTypesDto);

            var contentTypeService = new ContentTypeService(_repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.GetAllContentType();

            //Assert

            //Ensure that the result is not null
            Assert.NotNull(result);
            //Check the expected result count
            Assert.Equal(expectedContentTypesDto.Count(), result.Count());
        }

        [Fact]
        public async Task GetAllContentType_WithEmptyResultFromRepository_ReturnsEmptyCollection()
        {
            //Arrange

            //Settings for a mock methods that will return a list of objects
            var expectedContentTypes = new List<ContentType>();
            var expectedContentTypesDto = new List<ContentTypeDto>();

            _repositoryMock.Setup(u => u.Get())
                .ReturnsAsync(expectedContentTypes);

            _mapperMock.Setup(u => u.Map<IEnumerable<ContentTypeDto>>(expectedContentTypes))
                .Returns(expectedContentTypesDto);

            var contentTypeService = new ContentTypeService(_repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.GetAllContentType();

            //Assert

            //Ensure that the object count is zero 
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllContentType_WithNullResultFromRepository_ReturnsEmptyCollection()
        {
            //Arrange

            //Settings for a mock methods that will return a list of objects
            IEnumerable<ContentType> expectedContentTypes = null;

            _repositoryMock.Setup(u => u.Get())
                .ReturnsAsync(expectedContentTypes);

            var contentTypeService = new ContentTypeService(_repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.GetAllContentType();

            //Assert

            //Ensure that the result is null 
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        #endregion

        #region GetContentTypeByIdTests
        [Fact]
        public async Task GetContentTypeById_SuccessResult_ReturnsContentType()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            var expectedContentType = new ContentType { Id = 1, TypeName = "Test1" };
            var expectedContentTypeDto = new ContentTypeDto { Id = 1, TypeName = "Test1" };
            int testId = 1;

            _repositoryMock.Setup(u => u.GetById(testId))
                .ReturnsAsync(expectedContentType);

            _mapperMock.Setup(u => u.Map<ContentTypeDto>(expectedContentType))
                .Returns(expectedContentTypeDto);

            var contentTypeService = new ContentTypeService(_repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.GetContentTypeById(testId);

            //Assert

            //Ensure that the result type matches the expected type
            Assert.IsType<ContentTypeDto>(result);
            //Ensure that the expected id is equal result id  
            Assert.Equal(expectedContentType.Id, result.Id);
        }

        [Fact]
        public async Task GetContentTypeById_NullResultFromRepository_ThrowsKeyNotFoundException()
        {
            //Arrange

            //Settings for a mock methods that will return an object
            ContentType expectedContentType = null;
            int testId = 1;

            _repositoryMock.Setup(u => u.GetById(testId))
                .ReturnsAsync(expectedContentType);

            var categoryService = new ContentTypeService(_repositoryMock.Object, _mapperMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => categoryService.GetContentTypeById(testId));

        }
        #endregion

        #region CreateContentTypeTests
        [Fact]
        public async Task CreateContentType_SuccessResult_ReturnsTrue()
        {
            //Arrange

            //Settings for a mock methods that will return an boolean
            var expectedContentType = new ContentType { Id = 1, TypeName = "Test1" };
            var expectedContentTypeDto = new ContentTypeDto { Id = 1, TypeName = "Test1" };
            bool testResult = true;

            _repositoryMock.Setup(u => u.Create(expectedContentType))
                .ReturnsAsync(testResult);

            _mapperMock.Setup(u => u.Map<ContentType>(expectedContentTypeDto))
                .Returns(expectedContentType);

            var contentTypeService = new ContentTypeService(_repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.CreateContentType(expectedContentTypeDto);

            //Assert

            //Ensure that the result is true
            Assert.True(result);
        }

        [Fact]
        public async Task CreateContentType_WithFalseResultFromRepository_ReturnsFalse()
        {
            //Arrange

            //Settings for a mock methods that will return an boolean
            var expectedContentType = new ContentType { Id = 1, TypeName = "Test1" };
            var expectedContentTypeDto = new ContentTypeDto { Id = 1, TypeName = "Test1" };
            bool testResult = false;

            _repositoryMock.Setup(u => u.Create(expectedContentType))
                .ReturnsAsync(testResult);

            _mapperMock.Setup(u => u.Map<ContentType>(expectedContentTypeDto))
                .Returns(expectedContentType);

            var contentTypeService = new ContentTypeService(_repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.CreateContentType(expectedContentTypeDto);

            //Assert

            //Ensure that the result is false
            Assert.False(result);
        }
        #endregion

        #region UpdateContentTypeTests
        [Fact]
        public async Task UpdateContentType_SuccessResult_ReturnsTrue()
        {
            //Arrange

            //Settings for a mock methods that will return an boolean
            var expectedContentType = new ContentType { Id = 1, TypeName = "Test1" };
            var expectedContentTypeDto = new ContentTypeDto { Id = 1, TypeName = "Test1" };
            bool testResult = true;

            _repositoryMock.Setup(u => u.Update(expectedContentType))
                .ReturnsAsync(testResult);

            _mapperMock.Setup(u => u.Map<ContentType>(expectedContentTypeDto))
                .Returns(expectedContentType);

            var contentTypeService = new ContentTypeService(_repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.UpdateContentType(expectedContentTypeDto);

            //Assert

            //Ensure that the result is true
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateContentType_WithFalseResultFromRepository_ReturnsFalse()
        {
            //Arrange

            //Settings for a mock methods that will return an boolean
            var expectedContentType = new ContentType { Id = 1, TypeName = "Test1" };
            var expectedContentTypeDto = new ContentTypeDto { Id = 1, TypeName = "Test1" };
            bool testResult = false;

            _repositoryMock.Setup(u => u.Update(expectedContentType))
                .ReturnsAsync(testResult);

            _mapperMock.Setup(u => u.Map<ContentType>(expectedContentTypeDto))
                .Returns(expectedContentType);

            var contentTypeService = new ContentTypeService(_repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.UpdateContentType(expectedContentTypeDto);

            //Assert

            //Ensure that the result is false
            Assert.False(result);
        }
        #endregion

        #region DeleteContentTypeTests
        [Fact]
        public async Task DeleteContentType_SuccessResult_ReturnsTrue()
        {
            //Arrange

            //Settings for a mock method that will return an boolean
            int testId = 1;
            bool testResult = true;

            _repositoryMock.Setup(u => u.Delete(testId))
                .ReturnsAsync(testResult);

            var contentTypeService = new ContentTypeService(_repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.DeleteContentType(testId);

            //Assert

            //Ensure that the result is true
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteContentType_WithFalseResultFromRepository_ReturnsFalse()
        {
            //Arrange

            //Settings for a mock method that will return an boolean
            int testId = 1;
            bool testResult = false;

            _repositoryMock.Setup(u => u.Delete(testId))
                .ReturnsAsync(testResult);

            var contentTypeService = new ContentTypeService(_repositoryMock.Object, _mapperMock.Object);

            //Act
            var result = await contentTypeService.DeleteContentType(testId);

            //Assert

            //Ensure that the result is false
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

        private IEnumerable<ContentTypeDto> CreateTestContentTypeDto()
        {
            return new List<ContentTypeDto>
            {
                new ContentTypeDto {Id = 1, TypeName = "Test1"},
                new ContentTypeDto {Id = 2, TypeName = "Test2"},
                new ContentTypeDto {Id = 3, TypeName = "Test3"},

            };
        }
    }
}
