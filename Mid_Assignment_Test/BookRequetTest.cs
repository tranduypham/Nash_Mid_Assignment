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
    public class BookRequetTest : ControllerBase
    {
        const String VALID_TOKEN = "THIS_IS_FAKE_VALID_TOKEN";
        const String INVALID_TOKEN = "THIS_IS_FAKE_INVALID_TOKEN";


        [Fact]
        public void GetRequests_RandomUser_Return403()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = INVALID_TOKEN;
            var param = new RequestPaginateParameter();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(false);

            var expect = StatusCode(403);
            // When
            var result = controller.GetRequests(tokenAuth, param);

            // Then
            var res = Assert.IsType<ActionResult<IEnumerable<BookRequestWithDetail>> >(result);
            Assert.Equal(expect.GetType(), res.Result.GetType());
        }

        [Fact]
        public void GetRequests_AdminUser_ReturnOkList()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var param = new RequestPaginateParameter();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockBookRequestServices
                .Setup(x => x.showListRequest(param))
                .Returns(It.IsAny<PaginateList<BookRequestWithDetail>>());

            var expect = Ok("");
            // When
            var result = controller.GetRequests(tokenAuth, param);

            // Then
            var res = Assert.IsType<ActionResult<IEnumerable<BookRequestWithDetail>>>(result);
            Assert.Equal(expect.GetType() ,res.Result.GetType());
        }

        [Fact]
        public void GetRequestsDetail_RandomUser_Return403()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var id = It.IsAny<int>();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(false);

            // mockBookRequestServices
            //     .Setup(x => x.showListRequest(param))
            //     .Returns(It.IsAny<PaginateList<BookRequestWithDetail>>());

            var expect = StatusCode(403);
            // When
            var result = controller.GetRequestsDetail(id, tokenAuth);

            // Then
            var res = Assert.IsType<ActionResult<IEnumerable<BookBorrowingRequestDetail>>>(result);
            Assert.Equal(expect.GetType() ,res.Result.GetType());
        }

        [Fact]
        public void GetRequestsDetail_AdminUser_Return403()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var id = It.IsAny<int>();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);

            mockBookRequestServices
                .Setup(x => x.showRequestDetail(id))
                .Returns(new List<BookBorrowingRequestDetail>());

            var expect = It.IsAny<List<BookBorrowingRequestDetail>>();
            // When
            var result = controller.GetRequestsDetail(id, tokenAuth);

            // Then
            var res = Assert.IsType<ActionResult<IEnumerable<BookBorrowingRequestDetail>>>(result);
        }

        [Fact]
        public void PostRequests_RandomUser_ReturnBadWhenUser()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var request = It.IsAny<BookRequestWithDetail>();
            var userId = It.IsAny<int>();

            mockUserServices
                .Setup(x => x.getUserId(tokenAuth))
                .Returns(userId);

            var expect = BadRequest(new {Message = "Invalid Token"});
            var mess = expect.Value;
            // When
            var result = controller.PostRequests(request, tokenAuth);

            // Then
            var res = Assert.IsType<ActionResult<BookRequestWithDetail>>(result);
            var response = Assert.IsAssignableFrom<BadRequestObjectResult>(res.Result);
            Assert.Matches(mess.ToString(),response.Value.ToString());
        }

        [Fact]
        public void PostRequests_RandomUser_ReturnBadWhenOrder4OrdersInAMonth()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var request = It.IsAny<BookRequestWithDetail>();
            var userId = 1;

            mockUserServices
                .Setup(x => x.getUserId(tokenAuth))
                .Returns(userId);

            mockUserServices
                .Setup(x => x.listUserBookRequest(tokenAuth))
                .Returns(new List<BookBorrowingRequest>{
                    new BookBorrowingRequest(),
                    new BookBorrowingRequest(),
                    new BookBorrowingRequest(),
                    new BookBorrowingRequest()
                });

            var expect = BadRequest(new {Message = "You can not order in this month"});
            // When
            var result = controller.PostRequests(request, tokenAuth);

            // Then
            var res = Assert.IsType<ActionResult<BookRequestWithDetail>>(result);
            var response = Assert.IsAssignableFrom<BadRequestObjectResult>(res.Result);
            Assert.Matches(expect.Value.ToString(),response.Value.ToString());
        }

        [Fact]
        public void PostRequests_RandomUser_ReturnBadWhenOrder3OrdersInAMonth()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var request = It.IsAny<BookRequestWithDetail>();
            var userId = 1;

            mockUserServices
                .Setup(x => x.getUserId(tokenAuth))
                .Returns(userId);

            mockUserServices
                .Setup(x => x.listUserBookRequest(tokenAuth))
                .Returns(new List<BookBorrowingRequest>{
                    new BookBorrowingRequest(),
                    new BookBorrowingRequest(),
                    new BookBorrowingRequest()
                });

            var expect = BadRequest(new {Message = "You can not order in this month"});
            // When
            var result = controller.PostRequests(request, tokenAuth);

            // Then
            var res = Assert.IsType<ActionResult<BookRequestWithDetail>>(result);
            var response = Assert.IsAssignableFrom<BadRequestObjectResult>(res.Result);
            Assert.Matches(expect.Value.ToString(),response.Value.ToString());
        }

        [Fact]
        public void PostRequests_RandomUser_ReturnBadWhenOrder3Books()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var request = It.IsAny<BookRequestWithDetail>();
            var userId = 1;

            mockUserServices
                .Setup(x => x.getUserId(tokenAuth))
                .Returns(userId);

            mockUserServices
                .Setup(x => x.listUserBookRequest(tokenAuth))
                .Returns(new List<BookBorrowingRequest>{
                    new BookBorrowingRequest(),
                    new BookBorrowingRequest(),
                    new BookBorrowingRequest()
                });

            var expect = BadRequest(new {Message = "You can not order in this month"});
            // When
            var result = controller.PostRequests(request, tokenAuth);

            // Then
            var res = Assert.IsType<ActionResult<BookRequestWithDetail>>(result);
            var response = Assert.IsAssignableFrom<BadRequestObjectResult>(res.Result);
            Assert.Matches(expect.Value.ToString(),response.Value.ToString());
        }
        
        [Fact]
        public void PostRequests_RandomUser_ReturnBadWhenOrder0Book()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var listDetail = new List<BookBorrowingRequestDetail>{
                    // new BookBorrowingRequestDetail(),
                    // new BookBorrowingRequestDetail(),
                    // new BookBorrowingRequestDetail()
            };
            var request = new BookRequestWithDetail{
                ListDetail = listDetail
            };
            var userId = 1;

            mockUserServices
                .Setup(x => x.getUserId(tokenAuth))
                .Returns(userId);

            mockUserServices
                .Setup(x => x.listUserBookRequest(tokenAuth))
                .Returns(new List<BookBorrowingRequest>{
                    new BookBorrowingRequest(),
                    new BookBorrowingRequest()
                });

            var expect = BadRequest(new {Message = "You can not order more than 5 books"});
            // When
            var result = controller.PostRequests(request, tokenAuth);

            // Then
            var res = Assert.IsType<ActionResult<BookRequestWithDetail>>(result);
            var response = Assert.IsAssignableFrom<BadRequestObjectResult>(res.Result);
            Assert.Matches(expect.Value.ToString(),response.Value.ToString());
        }
        
        [Fact]
        public void PostRequests_RandomUser_ReturnBadWhenOrder6Books()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var listDetail = new List<BookBorrowingRequestDetail>{
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail()
            };
            var request = new BookRequestWithDetail{
                ListDetail = listDetail
            };
            var userId = 1;

            mockUserServices
                .Setup(x => x.getUserId(tokenAuth))
                .Returns(userId);

            mockUserServices
                .Setup(x => x.listUserBookRequest(tokenAuth))
                .Returns(new List<BookBorrowingRequest>{
                    new BookBorrowingRequest(),
                    new BookBorrowingRequest()
                });

            var expect = BadRequest(new {Message = "You can not order more than 5 books"});
            // When
            var result = controller.PostRequests(request, tokenAuth);

            // Then
            var res = Assert.IsType<ActionResult<BookRequestWithDetail>>(result);
            var response = Assert.IsAssignableFrom<BadRequestObjectResult>(res.Result);
            Assert.Matches(expect.Value.ToString(),response.Value.ToString());
        }
        
        [Fact]
        public void PostRequests_RandomUser_ReturnBadWhenOrder5BooksAndFailToSave()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var listDetail = new List<BookBorrowingRequestDetail>{
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail()
            };
            var requestWithDetail = new BookRequestWithDetail{
                ListDetail = listDetail
            };
            var userId = 1;

            mockUserServices
                .Setup(x => x.getUserId(tokenAuth))
                .Returns(userId);

            mockUserServices
                .Setup(x => x.listUserBookRequest(tokenAuth))
                .Returns(new List<BookBorrowingRequest>{
                    new BookBorrowingRequest(),
                    new BookBorrowingRequest()
                });

            mockBookRequestServices
                .Setup(x => x.createRequest(userId, requestWithDetail.BookRequest, requestWithDetail.ListDetail))
                .Returns(false);

            var expect = BadRequest(new {Message = "Some things went wrong"});
            // When
            var result = controller.PostRequests(requestWithDetail, tokenAuth);

            // Then
            var res = Assert.IsType<ActionResult<BookRequestWithDetail>>(result);
            var response = Assert.IsAssignableFrom<BadRequestObjectResult>(res.Result);
            Assert.Matches(expect.Value.ToString(),response.Value.ToString());
        }
        
        [Fact]
        public void PostRequests_RandomUser_ReturnOkWhenOrder5BooksAndSuccessToSave()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var listDetail = new List<BookBorrowingRequestDetail>{
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail(),
                    new BookBorrowingRequestDetail()
            };
            var requestWithDetail = new BookRequestWithDetail{
                ListDetail = listDetail
            };
            var userId = 1;

            mockUserServices
                .Setup(x => x.getUserId(tokenAuth))
                .Returns(userId);

            mockUserServices
                .Setup(x => x.listUserBookRequest(tokenAuth))
                .Returns(new List<BookBorrowingRequest>{
                    new BookBorrowingRequest(),
                    new BookBorrowingRequest()
                });

            mockBookRequestServices
                .Setup(x => x.createRequest(userId, requestWithDetail.BookRequest, requestWithDetail.ListDetail))
                .Returns(true);

            var expect = Ok(new {Message = "Create order success"});
            // When
            var result = controller.PostRequests(requestWithDetail, tokenAuth);

            // Then
            var res = Assert.IsType<ActionResult<BookRequestWithDetail>>(result);
            var response = Assert.IsAssignableFrom<OkObjectResult>(res.Result);
            Assert.Matches(expect.Value.ToString(),response.Value.ToString());
        }
        
        [Fact]
        public void ProcessOrder_RandomUser_Return403()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var id = 1;
            var request = new BookBorrowingRequest();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(false);

            var expect = StatusCode(403);
            // When
            var result = controller.ProcessOrder(id, tokenAuth, request);

            // Then
            var res = Assert.IsType<ActionResult<BookBorrowingRequest>>(result);
            var response = Assert.IsAssignableFrom<StatusCodeResult>(res.Result);
            Assert.Equal(expect.StatusCode,response.StatusCode);
        }
        
        [Fact]
        public void ProcessOrder_AdminUser_ReturnBadRequest()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var id = 1;
            var userId = 1;
            var request = new BookBorrowingRequest();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);
            
            mockUserServices
                .Setup(x => x.getUserId(tokenAuth))
                .Returns(userId);

            mockBookRequestServices
                .Setup(x => x.updateRequestState(id, request.State, userId))
                .Returns(false);

            var expect = BadRequest(new {Message = "Process fail"});
            // When
            var result = controller.ProcessOrder(id, tokenAuth, request);

            // Then
            var res = Assert.IsType<ActionResult<BookBorrowingRequest>>(result);
            var response = Assert.IsAssignableFrom<BadRequestObjectResult>(res.Result);
            Assert.Matches(expect.Value.ToString(),response.Value.ToString());
        }
        
        [Fact]
        public void ProcessOrder_AdminUser_ReturnOk()
        {
            // Given
            var mockBookRequestServices = new Mock<IBookRequestServices>();
            var mockUserServices = new Mock<IUserServices>();
            var controller = new BookRequestController(mockBookRequestServices.Object, mockUserServices.Object);
            var tokenAuth = VALID_TOKEN;
            var id = 1;
            var userId = 1;
            var request = new BookBorrowingRequest();

            mockUserServices
                .Setup(x => x.IsAdmin(tokenAuth))
                .Returns(true);
            
            mockUserServices
                .Setup(x => x.getUserId(tokenAuth))
                .Returns(userId);

            mockBookRequestServices
                .Setup(x => x.updateRequestState(id, request.State, userId))
                .Returns(true);

            var expect = Ok(new {Message = "Process success"});
            // When
            var result = controller.ProcessOrder(id, tokenAuth, request);

            // Then
            var res = Assert.IsType<ActionResult<BookBorrowingRequest>>(result);
            var response = Assert.IsAssignableFrom<OkObjectResult>(res.Result);
            Assert.Matches(expect.Value.ToString(),response.Value.ToString());
        }
    }
}