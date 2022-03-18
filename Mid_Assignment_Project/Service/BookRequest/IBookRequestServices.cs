using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Helper;

namespace Mid_Assignment_Project.Service
{
    public interface IBookRequestServices
    {
        public PaginateList<BookRequestWithDetail> showListRequest(RequestPaginateParameter param);
        public Boolean updateRequestState(int requestId, int state, int adminId);
        public List<BookBorrowingRequestDetail> showRequestDetail(int requestId);
        public Boolean createRequest(int userId, BookBorrowingRequest request, List<BookBorrowingRequestDetail> details);
    }
}