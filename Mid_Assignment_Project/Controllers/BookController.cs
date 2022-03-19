using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Service;
using System.Dynamic;

namespace Mid_Assignment_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBookServices _bookServices;
        private IUserServices _userServices;
        public BookController(IBookServices bookServices, IUserServices userServices)
        {
            _bookServices = bookServices;
            _userServices = userServices;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<BookWithCategory>> GetBooks([FromQuery] BookPaginateParameter bookParam)
        {
            var list = _bookServices.showList(bookParam);
            if (list != null)
            {
                var metadata = new
                {
                    list.CurrentPage,
                    list.TotalPage,
                    list.PageSize,
                    list.TotalCount,
                    list.HasNext,
                    list.HasPrevious
                };

                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
            }

            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id, [FromHeader] string tokenAuth)
        {
            bool isAdmin = _userServices.IsAdmin(tokenAuth);
            if (isAdmin)
            {
                var result = _bookServices.getBook(id);
                if (result == null) return BadRequest("Book not found");
                return Ok(result);
            }
            return StatusCode(403);
        }

        [HttpPost("")]
        public ActionResult<Book> PostBook(Book book, [FromHeader] string tokenAuth)
        {
            bool isAdmin = _userServices.IsAdmin(tokenAuth);
            if (isAdmin)
            {
                bool result = _bookServices.create(book);
                if (!result) return BadRequest(new { Message = "Create new Book Fail" });
                return Ok(new { Message = "Create new Book success" });
            }
            return StatusCode(403);
        }

        [HttpPut("{id}")]
        public ActionResult<Book> PutBook(int id, Book book, [FromHeader] string tokenAuth)
        {
            bool isAdmin = _userServices.IsAdmin(tokenAuth);
            if (isAdmin)
            {
                bool result = _bookServices.update(id, book);
                if (!result) return BadRequest(new { Message = "Update Book Fail" });
                return Ok(new { Message = "Update Book success" });
            }
            return StatusCode(403);
        }

        [HttpDelete("{id}")]
        public ActionResult<Book> DeleteBookById(int id, [FromHeader] string tokenAuth)
        {
            bool isAdmin = _userServices.IsAdmin(tokenAuth);
            if (isAdmin)
            {
                bool result = _bookServices.delete(id);
                if (!result) return BadRequest(new { Message = "Delete Book Fail" });
                return Ok(new { Message = "Delete Book success" });
            }
            return StatusCode(403);
        }
    }
}