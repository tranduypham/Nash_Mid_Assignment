using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mid_Assignment_Project.Models
{
    public class BookBorrowingRequest
    {
        public int BookBorrowingRequestId { get; set; } = 0;
        public int UserId { get; set; } = 0;
        public string Username { get; set; } = "";
        private byte state = 0;
        public byte State
        {
            get
            {
                return state;
            }
            set
            {
                int v = (value > 2 ? 2 :
                                        value < 0 ? 0 :
                                                        value);
                state = (byte)v;
            }
        }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public int AuthorizeBy { get; set; } = 0;
        public DateTime? AuthorizeDate { get; set; }
    }
}