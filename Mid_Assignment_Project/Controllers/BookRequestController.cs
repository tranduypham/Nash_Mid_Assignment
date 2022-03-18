using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mid_Assignment_Project.Service;
using Mid_Assignment_Project.Models;
using System.Text.Json;

namespace Mid_Assignment_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookRequestController : ControllerBase
    {
        private IBookRequestServices _bookRequestServices;
        private IUserServices _userServices;
        public BookRequestController(IBookRequestServices bookRequestServices, IUserServices userServices)
        {
            _bookRequestServices = bookRequestServices;
            _userServices = userServices;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<BookRequestWithDetail>> GetRequests([FromHeader] string tokenAuth, [FromQuery] RequestPaginateParameter param )
        {
            bool isAdmin = _userServices.IsAdmin(tokenAuth);
            if(isAdmin){
                var list = _bookRequestServices.showListRequest(param);
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
            return StatusCode(403);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<BookBorrowingRequestDetail>> GetRequestsDetail(int id, [FromHeader] string tokenAuth )
        {
            bool isAdmin = _userServices.IsAdmin(tokenAuth);
            if(isAdmin){
                return _bookRequestServices.showRequestDetail(id);
            }
            return StatusCode(403);
        }

        [HttpPost("")]
        public ActionResult<BookRequestWithDetail> PostRequests(BookRequestWithDetail requestWithDetail, [FromHeader] string tokenAuth )
        {
            // Check token -> get user ID
            int userId = _userServices.getUserId(tokenAuth);
            if(userId == null || userId <= 0) return BadRequest("Invalid Token");

            // Check user order in a month not pass 3 order
            var listUserRequest = _userServices.listUserBookRequest(tokenAuth);
            var today = DateTime.Now;
            var startMonth = new DateTime(today.Year, today.Month, 1);
            var endMonth = startMonth.AddMonths(1).AddDays(-1);
            int count = (from item in listUserRequest
                        where item.CreatedAt >= startMonth && item.CreatedAt <= endMonth
                        select item).Count();
            if(count < 0 || count >= 3) return BadRequest(new {Message = "You can not order in this month"});

            var request = requestWithDetail.BookRequest;
            var details = requestWithDetail.ListDetail;

            // Check if order has 1 to 5 books
            int countDetail = details.Count();
            if(countDetail == 0 || countDetail > 5) return BadRequest(new {Message = "You can not order more than 5 books"});

            // Create order
            bool result = _bookRequestServices.createRequest(userId, request, details);

            if(!result) return BadRequest(new {Message = "Some things went wrong"});
            return Ok(new {Message = "Create order success"});
        }


        [HttpPut("{id}")]
        public ActionResult<BookBorrowingRequest> ProcessOrder(int id, [FromHeader] string tokenAuth, [FromBody]BookBorrowingRequest state)
        {
            bool isAdmin = _userServices.IsAdmin(tokenAuth);
            if(isAdmin){
                int nextState = state.State;
                int adminId = _userServices.getUserId(tokenAuth);
                bool result = _bookRequestServices.updateRequestState(id, nextState, adminId);
                if(!result) return BadRequest(new {Message = "Process fail"});
                return Ok(new {Message = "Process success"});
            }
            return StatusCode(403);
        }

    //     [HttpGet("{id}")]
    //     public ActionResult<TModel> GetTModelById(int id)
    //     {
    //         return null;
    //     }

    //     [HttpPost("")]
    //     public ActionResult<TModel> PostTModel(TModel model)
    //     {
    //         return null;
    //     }

    //     [HttpPut("{id}")]
    //     public IActionResult PutTModel(int id, TModel model)
    //     {
    //         return NoContent();
    //     }

    //     [HttpDelete("{id}")]
    //     public ActionResult<TModel> DeleteTModelById(int id)
    //     {
    //         return null;
    //     }
    }
}