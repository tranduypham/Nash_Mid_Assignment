using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mid_Assignment_Project.Models
{
    public class BookRequestWithDetail
    {
        public BookBorrowingRequest BookRequest { get; set; }
        public List<BookBorrowingRequestDetail> ListDetail { get; set; }
    }
}