using Xunit;
using Moq;
using System;
using Microsoft.AspNetCore.Mvc;
using Mid_Assignment_Project.Service;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Controllers;

namespace Mid_Assignment_Test;

public class UserTest : ControllerBase
{
    // const BadRequestObjectResult BadRequest([ActionResultObjectValue] object? error);
    const String VALID_TOKEN = "THIS_IS_FAKE_VALID_TOKEN";
    const String INVALID_TOKEN = "THIS_IS_FAKE_INVALID_TOKEN";
    [Fact]
    public void Login_RandomUser_ReturnFalse()
    {
        // Arrange
        Token? token = null;
        var mockUserServices = new Mock<IUserServices>();
        mockUserServices
            .Setup(x => x.Login(It.IsAny<User>()))
            .Returns(token);


        var controller = new UserController(mockUserServices.Object);
        var expect = BadRequest(new { Message = "Login false" });

        // Act
        var response = controller.Login(It.IsAny<User>());

        // Assert
        var res = Assert.IsType<ActionResult<User>>(response);
        Assert.Equal(expect.ToString(), res.Result.ToString());
    }

    [Fact]
    public void Login_AdminUser_ReturnOkResponse()
    {
        // Arrange
        Token token = new Token
        {
            Payload = VALID_TOKEN
        };
        var mockUserServices = new Mock<IUserServices>();
        mockUserServices
            .Setup(x => x.Login(It.IsAny<User>()))
            .Returns(token);

        mockUserServices
            .Setup(x=>x.IsAdmin(VALID_TOKEN))
            .Returns(true);



        var controller = new UserController(mockUserServices.Object);
        var expect = Ok(new
        {
            TokenValue = token.Payload,
            Exp = token.CreatedAt.AddDays(3),
            IsAdmin = true
        });

        // Act
        var response = controller.Login(It.IsAny<User>());

        // Assert
        var res = Assert.IsType<ActionResult<User>>(response);
        Assert.Equal(expect.ToString(), res.Result.ToString());
    }

    [Fact]
    public void Login_NormalUser_ReturnOkResponse()
    {
        // Arrange
        Token token = new Token
        {
            Payload = VALID_TOKEN
        };
        var mockUserServices = new Mock<IUserServices>();
        mockUserServices
            .Setup(x => x.Login(It.IsAny<User>()))
            .Returns(token);

        mockUserServices
            .Setup(x=>x.IsAdmin(VALID_TOKEN))
            .Returns(false);



        var controller = new UserController(mockUserServices.Object);
        var expect = Ok(new
        {
            TokenValue = token.Payload,
            Exp = token.CreatedAt.AddDays(3),
            IsAdmin = false
        });

        // Act
        var response = controller.Login(It.IsAny<User>());

        // Assert
        var res = Assert.IsType<ActionResult<User>>(response);
        Assert.Equal(expect.ToString(), res.Result.ToString());
    }

    [Fact]
    public void Verify_InvalidToken_ReturnBadRequestResponse()
    {
        // Arrange
        var mockUserServices = new Mock<IUserServices>();
        mockUserServices
            .Setup(x => x.IsValid(INVALID_TOKEN))
            .Returns(false);

        var controller = new UserController(mockUserServices.Object);
        var expect = BadRequest(new { Message = "please logout" });

        // Act
        var response = controller.Verify(INVALID_TOKEN);

        // Assert
        var res = Assert.IsType<ActionResult<User>>(response);
        Assert.Equal(expect.ToString(), res.Result.ToString());
    }

    [Fact]
    public void Verify_ValidToken_ReturnOkResponse()
    {
        // Arrange
        var mockUserServices = new Mock<IUserServices>();
        mockUserServices
            .Setup(x => x.IsValid(VALID_TOKEN))
            .Returns(true);

        var controller = new UserController(mockUserServices.Object);
        var expect = Ok(new { Message = "token valid" });

        // Act
        var response = controller.Verify(VALID_TOKEN);

        // Assert
        var res = Assert.IsType<ActionResult<User>>(response);
        Assert.Equal(expect.ToString(), res.Result.ToString());
    }

    [Fact]
    public void Logout_InvalidToken_ReturnBadRequestResponse()
    {
        // Arrange
        var mockUserServices = new Mock<IUserServices>();
        mockUserServices
            .Setup(x => x.Logout(INVALID_TOKEN))
            .Returns(false);

        var controller = new UserController(mockUserServices.Object);
        var expect = BadRequest(new { Message = "Logout false" });

        // Act
        var response = controller.Logout(INVALID_TOKEN);

        // Assert
        var res = Assert.IsType<ActionResult<User>>(response);
        Assert.Equal(expect.ToString(), res.Result.ToString());
    }

    [Fact]
    public void Logout_ValidToken_ReturnOkResponse()
    {
        // Arrange
        var mockUserServices = new Mock<IUserServices>();
        mockUserServices
            .Setup(x => x.Logout(VALID_TOKEN))
            .Returns(true);

        var controller = new UserController(mockUserServices.Object);
        var expect = Ok(new { Message = "Bye Bye" });

        // Act
        var response = controller.Logout(VALID_TOKEN);

        // Assert
        var res = Assert.IsType<ActionResult<User>>(response);
        Assert.Equal(expect.ToString(), res.Result.ToString());
    }
}