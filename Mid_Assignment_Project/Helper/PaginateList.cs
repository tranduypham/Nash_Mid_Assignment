using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mid_Assignment_Project.Helper
{
    public class PaginateList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPage;
        public PaginateList(List<T> list, int totalPageCount, int currentPage, int pageSize)
        {
            TotalCount = totalPageCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
            TotalPage = (int)Math.Ceiling(totalPageCount / (double)pageSize);

            AddRange(list); // add the #1 param to the paginate list
        }

        public static PaginateList<T> paginated (List<T> source, int currentPage, int pageSize){
            var count = source.Count();
            var list = source.Skip((currentPage - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();
            return new PaginateList<T>(list, count, currentPage, pageSize);
        }
    }
}