using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Repository
{
    public class BookBorrowingRequestRepository : IBookBorrowingRequestRepository
    {
        private LibaryDbContext _libDbContext;
        private TransactionManager _transMana;
        public BookBorrowingRequestRepository(LibaryDbContext libDbContext)
        {
            _libDbContext = libDbContext;
            _transMana = new TransactionManager(libDbContext);
        }

        private int createRequest(int userId, BookBorrowingRequest request)
        {
            var bookRequest = request;
            bookRequest.BookBorrowingRequestId = 0;
            bookRequest.AuthorizeBy = 0;
            bookRequest.State = 0;
            bookRequest.UserId = userId;
            var user = _libDbContext.Users.Find(userId);
            bookRequest.Username = user == null ? "no name" : user.Username ;
            _libDbContext.BookBorrowingRequests.Add(bookRequest);
            _libDbContext.SaveChanges();
            // var savedRequest = _libDbContext.BookBorrowingRequests.Last();
            return bookRequest.BookBorrowingRequestId;
        }

        private void createRequestDetail(int requestId, BookBorrowingRequestDetail detail)
        {
            var book = _libDbContext.Books.Find(detail.BookId);
            if(book != null) {
                var bookRequestdetail = detail;
                // bookRequestdetail.BookBorrowingRequestDetailId = 0;
                bookRequestdetail.BookBorrowingRequestId = requestId;
                bookRequestdetail.BookId = detail.BookId;
                bookRequestdetail.BookName = book.BookName;
                _libDbContext.BookBorrowingRequestDetails.Add(bookRequestdetail);
                _libDbContext.SaveChanges();
            }
            else {
                throw new Exception("Book not found");
            }
            // var savedRequest = bookRequestdetail;
            return;
        }

        public bool createRequestWithDetail(int userId, BookBorrowingRequest request, List<BookBorrowingRequestDetail> details)
        {
            return _transMana.transactionManager(() =>
            {
                int bookRequestId = createRequest(userId, request);
                foreach(BookBorrowingRequestDetail detail in details){
                    createRequestDetail(bookRequestId, detail);
                }
            });
        }

        public List<BookBorrowingRequest> showListRequest()
        {
            return _libDbContext.BookBorrowingRequests.ToList();
        }

        public List<BookBorrowingRequest> showUserListRequest(int userId){
            return _libDbContext.BookBorrowingRequests.Where(r => r.UserId == userId).ToList();
        }

        public BookBorrowingRequest? showRequest(int requestId)
        {
            return _libDbContext.BookBorrowingRequests.Find(requestId);
        }

        public List<BookBorrowingRequestDetail> showRequestDetail(int requestId)
        {
            return _libDbContext.BookBorrowingRequestDetails.Where(detail => detail.BookBorrowingRequestId == requestId).ToList();
        }

        public bool updateRequest(int requestId, int state, int adminId)
        {
            return _transMana.transactionManager(() =>
            {
                var bookRequest = _libDbContext.BookBorrowingRequests.Find(requestId);
                if (bookRequest == null)
                {
                    throw new Exception("It null here");
                }
                bookRequest.State = (byte)state;
                bookRequest.AuthorizeBy = adminId;
                bookRequest.AuthorizeDate = DateTime.Now;

                _libDbContext.SaveChanges();
            });
        }
    }
}