using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Helper;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Repository;

namespace Mid_Assignment_Project.Service
{
    public class BookRequestServices : IBookRequestServices
    {
        private IBookBorrowingRequestRepository _bookRequestRepo;
        
        public BookRequestServices(IBookBorrowingRequestRepository bookRequestRepo)
        {
            _bookRequestRepo = bookRequestRepo;
        }
        public bool createRequest(int userId, BookBorrowingRequest request, List<BookBorrowingRequestDetail> details)
        {
            return _bookRequestRepo.createRequestWithDetail(userId, request, details);
        }

        public PaginateList<BookRequestWithDetail> showListRequest(RequestPaginateParameter param)
        {
            List<BookRequestWithDetail> listWithDetail = new List<BookRequestWithDetail>();
            var listRequests = _bookRequestRepo.showListRequest();
            foreach(BookBorrowingRequest request in listRequests){
                var listDetail = _bookRequestRepo.showRequestDetail(request.BookBorrowingRequestId);
                var requestWithDetail = new BookRequestWithDetail{
                    BookRequest = request,
                    ListDetail = listDetail
                };
                listWithDetail.Add(requestWithDetail);
            }
            PaginateList<BookRequestWithDetail> paginateList = PaginateList<BookRequestWithDetail>.paginated(listWithDetail, param.CurrentPage, param.PageSize);
            return paginateList;
        }

        public List<BookBorrowingRequestDetail> showRequestDetail(int requestId)
        {
            return _bookRequestRepo.showRequestDetail(requestId);
        }

        public bool updateRequestState(int requestId, int state, int adminId)
        {
            return _bookRequestRepo.updateRequest(requestId, state, adminId);
        }
    }
}