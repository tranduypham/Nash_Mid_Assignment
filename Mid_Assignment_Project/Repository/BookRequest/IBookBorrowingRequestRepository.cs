using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Repository
{
    public interface IBookBorrowingRequestRepository
    {
        public List<BookBorrowingRequest> showListRequest();
        public List<BookBorrowingRequest> showUserListRequest(int userId);
        // public Boolean createRequest(int userId, BookBorrowingRequest request);
        // public Boolean createRequestDetail(int requestId, BookBorrowingRequestDetail detail);
        public Boolean createRequestWithDetail(int userId, BookBorrowingRequest request, List<BookBorrowingRequestDetail> details);
        public Boolean updateRequest(int requestId, int state, int adminId);
        public BookBorrowingRequest showRequest(int requestId);
        public List<BookBorrowingRequestDetail> showRequestDetail(int requestId);
    }
}