using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Repository
{
    public interface IBookRepository
    {
        public List<Book> showList();
        // public List<Book> showBook (int cateId);
        public Boolean create(Book book);
        public Boolean update(int id, Book book);
        public Boolean delete(int id);
        public Category getCategory(int id);
        public Book getBook(int id);
    }
}