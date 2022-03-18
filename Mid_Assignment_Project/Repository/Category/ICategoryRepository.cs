using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Repository
{
    public interface ICategoryRepository
    {
        public List<Category> showList ();
        public List<Book> showBook (int cateId);
        public Category showCategory (int cateId);
        public Boolean create(Category cate);
        public Boolean update(int id, Category cate);
        public Boolean delete(int id);
    }
}