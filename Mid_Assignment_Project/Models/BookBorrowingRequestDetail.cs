using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mid_Assignment_Project.Models
{
    public class BookBorrowingRequestDetail
    {
        public int BookBorrowingRequestDetailId { get; set; } = 0;
        public int BookBorrowingRequestId { get; set; } = 0;
        public int BookId { get; set; }
        public string BookName { get; set; } = "";
    }
}