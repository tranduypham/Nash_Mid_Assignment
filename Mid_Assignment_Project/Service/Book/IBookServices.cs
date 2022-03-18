using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Helper;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Service
{
    public interface IBookServices
    {
        public PaginateList<BookWithCategory> showList (BookPaginateParameter bookPage);
        // public List<Book> showBook (int bookId);
        public Boolean create(Book book);
        public Boolean update(int id, Book book);
        public Boolean delete(int id);
        public Book getBook(int id);
    }
}