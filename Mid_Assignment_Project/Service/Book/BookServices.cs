using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Helper;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Repository;

namespace Mid_Assignment_Project.Service
{
    public class BookServices : IBookServices
    {
        private IBookRepository _bookRepo;
        public BookServices(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public bool create(Book book)
        {
            return _bookRepo.create(book);
        }

        public bool delete(int id)
        {
            return _bookRepo.delete(id);
        }

        public Book getBook(int id)
        {
            return _bookRepo.getBook(id);
        }

        // public List<Book> showBook(int bookId)
        // {
        //     throw new NotImplementedException();
        // }

        public PaginateList<BookWithCategory> showList(BookPaginateParameter bookPageParam)
        {
            List<BookWithCategory> resultList = new List<BookWithCategory>();
            List<Book> list = _bookRepo.showList();
            foreach(Book book in list){
                var cateId = book.CategoryId;
                var cate = _bookRepo.getCategory(cateId);
                var item = new BookWithCategory{
                    BookItem = book,
                    CategoryOfBook = cate
                };
                resultList.Add(item);
            }
            PaginateList<BookWithCategory> paginateList = PaginateList<BookWithCategory>.paginated(resultList, bookPageParam.CurrentPage, bookPageParam.PageSize);
            return paginateList;
        }

        public bool update(int id, Book book)
        {
            return _bookRepo.update(id, book);
        }
    }
}