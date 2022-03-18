using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Service;

namespace Mid_Assignment_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("login")]
        public ActionResult<User> Login(User user)
        {
            Token token = _userServices.Login(user);
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            if (token != null)
            {
                return Ok(new
                {
                    TokenValue = token.Payload,
                    Exp = token.CreatedAt.AddDays(3),
                    IsAdmin = _userServices.IsAdmin(token.Payload)
                });
            }
            return BadRequest(new { Message = "Login false" });
        }

        [HttpPost("verify")]
        public ActionResult<User> Verify([FromHeader] string tokenAuth)
        {
            bool isValid = _userServices.IsValid(tokenAuth);
            if (isValid)
            {
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return Ok(new { Message = "token valid" });
            }
            return BadRequest(new { Message = "please logout" });
        }


        [HttpPost("logout")]
        public ActionResult<User> Logout(String token)
        {
            bool result = _userServices.Logout(token);
            if (result)
            {
                return Ok(new
                {
                    Message = "Bye Bye"
                });
            }
            return BadRequest(new { Message = "Logout false" });
        }
        // [HttpGet("")]
        // public ActionResult<IEnumerable<User>> GetUsers()
        // {
        //     return new List<User> { };
        // }

        // [HttpGet("{id}")]
        // public ActionResult<User> GetUserById(int id)
        // {
        //     return null;
        // }

        // [HttpPost("")]
        // public ActionResult<User> PostUser(User model)
        // {
        //     return null;
        // }

        // [HttpPut("{id}")]
        // public IActionResult PutUser(int id, User model)
        // {
        //     return NoContent();
        // }

        // [HttpDelete("{id}")]
        // public ActionResult<User> DeleteUserById(int id)
        // {
        //     return null;
        // }
    }
}