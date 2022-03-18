using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mid_Assignment_Project.Models
{
    public class Category
    {
        public int CategoryId { get; set; } 
        public string CategoryName { get; set; } = "no name";
        public DateTime CreatedAt { get; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}