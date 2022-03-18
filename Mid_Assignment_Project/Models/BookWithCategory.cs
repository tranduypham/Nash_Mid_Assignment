using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mid_Assignment_Project.Models
{
    public class BookWithCategory
    {
        public Book BookItem { get; set; }
        public Category CategoryOfBook { get; set; }
    }
}