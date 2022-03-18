using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Helper;

namespace Mid_Assignment_Project.Service
{
    public interface ICategoryServices
    {
        public PaginateList<Category> showList (CategoryPaginateParameter catePage);
        public PaginateList<BookWithCategory> showBook (int cateId, CategoryPaginateParameter catePage);
        public Category showCategory (int cateId);
        public Boolean create(Category cate);
        public Boolean update(int id, Category cate);
        public Boolean delete(int id);
    }
}