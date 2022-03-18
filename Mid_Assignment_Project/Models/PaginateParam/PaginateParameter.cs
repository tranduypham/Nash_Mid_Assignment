using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mid_Assignment_Project.Models
{
    public abstract class PaginateParameter
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}