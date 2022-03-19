using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mid_Assignment_Project.Service;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Controllers;
using Mid_Assignment_Project.Helper;
using System.Collections.Generic;
using Xunit;
using Moq;

namespace Mid_Assignment_Test
{
    public class CategoryTest : ControllerBase
    {
        const String VALID_TOKEN = "THIS_IS_FAKE_VALID_TOKEN";
        const String INVALID_TOKEN = "THIS_IS_FAKE_INVALID_TOKEN";

        [Fact]
        public void GetCategorys_RandomUser_ReturnList()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);
            var catePaginateParam = It.IsAny<CategoryPaginateParameter>();

            mockCategoryServices
                .Setup(x => x.showList(catePaginateParam))
                .Returns(It.IsAny<PaginateList<Category>>);

            var expect = Ok(It.IsAny<PaginateList<Category>>());
            // When
            var result = controller.GetCategorys(catePaginateParam);
            // Then
            var res = Assert.IsType<ActionResult<IEnumerable<Category>>>(result);
            Assert.Equal(res.Result.ToString(), expect.ToString());
        }

        [Fact]
        public void GetBookByCategoryId_RandomUser_ReturnList()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var catePaginateParam = It.IsAny<CategoryPaginateParameter>();
            var id = It.IsAny<int>();

            mockCategoryServices
                .Setup(x => x.showBook(id, catePaginateParam))
                .Returns(It.IsAny<PaginateList<BookWithCategory>>);

            var expect = Ok(It.IsAny<PaginateList<Book>>());
            // When
            var result = controller.GetBookByCategoryId(id, catePaginateParam);
            // Then
            var res = Assert.IsType<ActionResult<IEnumerable<BookWithCategory>>>(result);
            Assert.Equal(expect.GetType(), res.Result.GetType());
            Assert.Equal(expect.Value, res.Value);
        }

        [Fact]
        public void GetCategoryId_RandomUser_Return403Response()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var id = It.IsAny<int>();
            var tokenAuth = INVALID_TOKEN;

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(false);

            mockCategoryServices
                .Setup(x => x.showCategory(id))
                .Returns(It.IsAny<Category>);

            var expect = StatusCode(403);
            // When
            var result = controller.GetCategoryId(id, tokenAuth);
            // Then
            var res = Assert.IsType<ActionResult<Category>>(result);
            Assert.Equal(expect.GetType(), res.Result.GetType());
        }

        [Fact]
        public void GetCategoryId_AdminUser_ReturnOkResponse()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var id = It.IsAny<int>();
            var tokenAuth = INVALID_TOKEN;
            var category = new Category();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockCategoryServices
                .Setup(x => x.showCategory(id))
                .Returns(category);

            var expect = Ok(category);
            // When
            var result = controller.GetCategoryId(id, tokenAuth);
            // Then
            var res = Assert.IsType<ActionResult<Category>>(result);
            Assert.Equal(expect.GetType(), res.Result.GetType());
        }

        [Fact]
        public void GetCategoryId_AdminUser_ReturnBadRequestResponse()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var id = It.IsAny<int>();
            var tokenAuth = INVALID_TOKEN;
            Category? category = null;

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockCategoryServices
                .Setup(x => x.showCategory(id))
                .Returns(category);

            var expect = BadRequest("");
            // When
            var result = controller.GetCategoryId(id, tokenAuth);
            // Then
            var res = Assert.IsType<ActionResult<Category>>(result);
            Assert.Equal(expect.GetType(), res.Result.GetType());
        }

        [Fact]
        public void CreateCategory_RandomUser_Return403Response()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var tokenAuth = INVALID_TOKEN;
            Category category = new Category();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(false);

            // mockCategoryServices
            //     .Setup(x => x.showCategory(id))
            //     .Returns(category);

            var expect = StatusCode(403);
            // When
            var result = controller.CreateCategory(category, tokenAuth);
            // Then
            var res = Assert.IsType<ActionResult<Category>>(result);
            Assert.Equal(expect.GetType(), res.Result.GetType());
        }

        [Fact]
        public void CreateCategory_AdminUser_ReturnOkResponse()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var tokenAuth = VALID_TOKEN;
            Category category = new Category();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockCategoryServices
                .Setup(x => x.create(category))
                .Returns(true);

            var expect = Ok("");
            // When
            var result = controller.CreateCategory(category, tokenAuth);
            // Then
            var res = Assert.IsType<ActionResult<Category>>(result);
            Assert.Equal(expect.GetType(), res.Result.GetType());
        }

        [Fact]
        public void CreateCategory_AdminUser_ReturnBadRequestResponse()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var tokenAuth = VALID_TOKEN;
            Category category = new Category();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockCategoryServices
                .Setup(x => x.create(category))
                .Returns(false);

            var expect = BadRequest("");
            // When
            var result = controller.CreateCategory(category, tokenAuth);
            // Then
            var res = Assert.IsType<ActionResult<Category>>(result);
            Assert.Equal(expect.GetType(), res.Result.GetType());
        }

        [Fact]
        public void PutCategory_RandomUser_Return403Response()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var id = It.IsAny<int>();
            var tokenAuth = INVALID_TOKEN;
            Category category = new Category();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(false);

            // mockCategoryServices
            //     .Setup(x => x.create(category))
            //     .Returns(false);

            var expect = StatusCode(403);
            // When
            var result = controller.PutCategory(id, category, tokenAuth);
            // Then
            var res = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(expect.GetType(), res.GetType());
        }

        [Fact]
        public void PutCategory_AdminUser_ReturnBadResponse()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var id = It.IsAny<int>();
            var tokenAuth = VALID_TOKEN;
            Category category = new Category();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockCategoryServices
                .Setup(x => x.update(id, category))
                .Returns(false);

            var expect = BadRequest(new { Message = "Update category fail" });
            // When
            var result = controller.PutCategory(id, category, tokenAuth);
            // Then
            var res = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(expect.StatusCode, res.StatusCode);
        }

        [Fact]
        public void PutCategory_AdminUser_ReturnOkResponse()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var id = It.IsAny<int>();
            var tokenAuth = VALID_TOKEN;
            Category category = new Category();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockCategoryServices
                .Setup(x => x.update(id, category))
                .Returns(true);

            var expect = Ok("");
            // When
            var result = controller.PutCategory(id, category, tokenAuth);
            // Then
            var res = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expect.StatusCode, res.StatusCode);
        }

        [Fact]
        public void DeleteCategoryById_RandomUser_Return403Response()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var id = It.IsAny<int>();
            var tokenAuth = INVALID_TOKEN;

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(false);

            mockCategoryServices
                .Setup(x => x.delete(id))
                .Returns(false);

            var expect = StatusCode(403);
            // When
            var result = controller.DeleteCategoryById(id, tokenAuth);
            // Then
            var res = Assert.IsType<ActionResult<Category>>(result);
            Assert.Equal(expect.GetType(), res.Result.GetType());
        }

        [Fact]
        public void DeleteCategoryById_AdminUser_ReturnBadRequestResponse()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var id = It.IsAny<int>();
            var tokenAuth = VALID_TOKEN;

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockCategoryServices
                .Setup(x => x.delete(id))
                .Returns(false);

            var expect = BadRequest("");
            // When
            var result = controller.DeleteCategoryById(id, tokenAuth);
            // Then
            var res = Assert.IsType<ActionResult<Category>>(result);
            Assert.Equal(expect.GetType(), res.Result.GetType());
        }

        [Fact]
        public void DeleteCategoryById_AdminUser_ReturnOkResponse()
        {
            // Given
            var mockCategoryServices  = new Mock<ICategoryServices>();
            var mockUserServices  = new Mock<IUserServices>();
            var controller = new CategoryController(mockCategoryServices.Object, mockUserServices.Object);

            var id = It.IsAny<int>();
            var tokenAuth = VALID_TOKEN;

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockCategoryServices
                .Setup(x => x.delete(id))
                .Returns(true);

            var expect = Ok("");
            // When
            var result = controller.DeleteCategoryById(id, tokenAuth);
            // Then
            var res = Assert.IsType<ActionResult<Category>>(result);
            Assert.Equal(expect.GetType(), res.Result.GetType());
        }
    }
}