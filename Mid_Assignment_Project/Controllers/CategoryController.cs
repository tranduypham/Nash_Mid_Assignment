using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Service;
using Mid_Assignment_Project.Helper;

namespace Mid_Assignment_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryServices _cateServices;
        private IUserServices _userServices;
        public CategoryController(ICategoryServices cateServices, IUserServices userServices)
        {
            _cateServices = cateServices;
            _userServices = userServices;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Category>> GetCategorys([FromQuery] CategoryPaginateParameter catePage)
        {
            PaginateList<Category> list = _cateServices.showList(catePage);
            var metadata = new {
                list.CurrentPage,
                list.TotalPage,
                list.PageSize,
                list.TotalCount,
                list.HasNext,
                list.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
            return Ok(list);
        }

        [HttpGet("books/{id}")]
        public ActionResult<IEnumerable<BookWithCategory>> GetBookByCategoryId(int id, [FromQuery] CategoryPaginateParameter catePage)
        {
            var list = _cateServices.showBook(id, catePage);
            var metadata = new {
                list.CurrentPage,
                list.TotalPage,
                list.PageSize,
                list.TotalCount,
                list.HasNext,
                list.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategoryId(int id, [FromHeader] string tokenAuth)
        {
            bool isAdmin = _userServices.IsAdmin(tokenAuth);

            if (isAdmin){
                var category = _cateServices.showCategory(id);
                if(category == null) return BadRequest("Category not found");
                return Ok(category);
            }
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        [HttpPost("")]
        public ActionResult<Category> CreateCategory(Category cate, [FromHeader] string tokenAuth)
        {
            bool isAdmin = _userServices.IsAdmin(tokenAuth);

            if (isAdmin)
            {
                bool result = _cateServices.create(cate);
                if (!result) return BadRequest(new { Message = "Create new category fail" });
                return Ok(new { Message = "Create new category successfully" });
            }

            // return Problem(
            //     title: "User is not authorized.",
            //     statusCode: StatusCodes.Status403Forbidden
            // );
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        [HttpPut("{id}")]
        public IActionResult PutCategory(int id, Category cate, [FromHeader] string tokenAuth)
        {
            bool isAdmin = _userServices.IsAdmin(tokenAuth);
            if (isAdmin) {
                bool result = _cateServices.update(id, cate);
                if (!result) return BadRequest(new { Message = "Update category fail" });
                return Ok(new { Message = "Update category successfully" });
            }
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        [HttpDelete("{id}")]
        public ActionResult<Category> DeleteCategoryById(int id, [FromHeader] string tokenAuth)
        {
            bool isAdmin = _userServices.IsAdmin(tokenAuth);
            if (isAdmin){
                bool result = _cateServices.delete(id);
                if (!result) return BadRequest(new { Message = "Delete category fail" });
                return Ok(new { Message = "Delete category successfully" });
            }
            return StatusCode(StatusCodes.Status403Forbidden);
        }
    }
}