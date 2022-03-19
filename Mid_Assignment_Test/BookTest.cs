using Xunit;
using Moq;
using System;
using Microsoft.AspNetCore.Mvc;
using Mid_Assignment_Project.Service;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Controllers;
using Mid_Assignment_Project.Helper;
using System.Collections.Generic;

namespace Mid_Assignment_Test
{
    public class BookTest : ControllerBase
    {
        const String VALID_TOKEN = "THIS_IS_FAKE_VALID_TOKEN";
        const String INVALID_TOKEN = "THIS_IS_FAKE_INVALID_TOKEN";

        [Fact]
        public void GetBooks_RandomUser_ReturnBookList()
        {
            // Arrange
            var bookParam = new BookPaginateParameter();
            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockBookServices
                .Setup(x => x.showList(bookParam))
                .Returns(It.IsAny<PaginateList<BookWithCategory>>);


            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = Ok(It.IsAny<PaginateList<BookWithCategory>>);

            // Act
            var response = controller.GetBooks(bookParam);

            // Assert
            var res = Assert.IsType<ActionResult<IEnumerable<BookWithCategory>>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }

        [Fact]
        public void GetBookById_AdminUser_ReturnABook()
        {
            // Arrange
            var id = It.IsAny<int>();
            var tokenAuth = VALID_TOKEN;
            var book = new Book();
            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockBookServices
                .Setup(x => x.getBook(id))
                .Returns(book);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = Ok(book);

            // Act
            var response = controller.GetBookById(id, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }

        [Fact]
        public void GetBookById_NormalUser_ReturnABook()
        {
            // Arrange
            var id = It.IsAny<int>();
            var tokenAuth = VALID_TOKEN;
            var book = new Book();
            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(false);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = StatusCode(403);

            // Act
            var response = controller.GetBookById(id, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }

        [Fact]
        public void GetBookById_AdminUser_ReturnNull()
        {
            // Arrange
            var id = It.IsAny<int>();
            var tokenAuth = VALID_TOKEN;
            Book? book = null;
            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockBookServices
                .Setup(x => x.getBook(id))
                .Returns(book);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = BadRequest("");

            // Act
            var response = controller.GetBookById(id, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.GetType(), res.Result.GetType());
        }

        [Fact]
        public void PostBook_AdminUser_ReturnSuccessResponse()
        {
            // Arrange
            var tokenAuth = VALID_TOKEN;
            Book book = It.IsAny<Book>();
            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockBookServices
                .Setup(x => x.create(book))
                .Returns(true);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = Ok("");

            // Act
            var response = controller.PostBook(book, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }

        [Fact]
        public void PostBook_AdminUser_ReturnBadResponse()
        {
            // Arrange
            var tokenAuth = VALID_TOKEN;
            Book book = It.IsAny<Book>();
            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockBookServices
                .Setup(x => x.create(book))
                .Returns(false);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = BadRequest("");

            // Act
            var response = controller.PostBook(book, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }

        [Fact]
        public void PostBook_RandomUser_Return403Response()
        {
            // Arrange
            var tokenAuth = INVALID_TOKEN;
            Book book = It.IsAny<Book>();
            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(false);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = StatusCode(403);

            // Act
            var response = controller.PostBook(book, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }

        [Fact]
        public void PutBook_AdminUser_ReturnOkResponse()
        {
            // Arrange
            var tokenAuth = VALID_TOKEN;
            Book book = It.IsAny<Book>();
            int id = It.IsAny<int>();

            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockBookServices
                .Setup(x => x.update(id,book))
                .Returns(true);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = Ok("");

            // Act
            var response = controller.PutBook(id, book, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }

        [Fact]
        public void PutBook_AdminUser_ReturnBadRequestResponse()
        {
            // Arrange
            var tokenAuth = VALID_TOKEN;
            Book book = It.IsAny<Book>();
            int id = It.IsAny<int>();

            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockBookServices
                .Setup(x => x.update(id,book))
                .Returns(false);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = BadRequest("");

            // Act
            var response = controller.PutBook(id, book, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }

        [Fact]
        public void PutBook_RandomUser_Return403Response()
        {
            // Arrange
            var tokenAuth = INVALID_TOKEN;
            Book book = It.IsAny<Book>();
            int id = It.IsAny<int>();

            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(false);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = StatusCode(403);

            // Act
            var response = controller.PutBook(id, book, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }

        [Fact]
        public void DeleteBookById_RandomUser_Return403Response()
        {
            // Arrange
            var tokenAuth = INVALID_TOKEN;
            int id = It.IsAny<int>();

            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(false);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = StatusCode(403);

            // Act
            var response = controller.DeleteBookById(id, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }

        [Fact]
        public void DeleteBookById_AdminUser_ReturnOkResponse()
        {
            // Arrange
            var tokenAuth = INVALID_TOKEN;
            int id = It.IsAny<int>();

            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockBookServices
                .Setup(x => x.delete(id))
                .Returns(true);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = Ok("");

            // Act
            var response = controller.DeleteBookById(id, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }

        [Fact]
        public void DeleteBookById_AdminUser_Return403Response()
        {
            // Arrange
            var tokenAuth = INVALID_TOKEN;
            int id = It.IsAny<int>();

            var mockUserServices = new Mock<IUserServices>();
            var mockBookServices = new Mock<IBookServices>();
            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);
                
            mockBookServices
                .Setup(x => x.delete(id))
                .Returns(false);

            var controller = new BookController(mockBookServices.Object, mockUserServices.Object);
            var expect = BadRequest("");

            // Act
            var response = controller.DeleteBookById(id, tokenAuth);

            // Assert
            var res = Assert.IsType<ActionResult<Book>>(response);
            Assert.Equal(expect.ToString(), res.Result.ToString());
        }
    }
}