using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mid_Assignment_Project.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string BookName { get; set; } = "no name";
        public int CategoryId { get; set; } = 1;
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}