using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Repository
{
    public class BookRepository : IBookRepository
    {
        private LibaryDbContext _libDbContext;
        private TransactionManager _transactionManager;
        public BookRepository(LibaryDbContext libDbContext)
        {
            _libDbContext = libDbContext;
            _transactionManager = new TransactionManager(_libDbContext);
        }

        public bool create(Book book)
        {
            return _transactionManager.transactionManager(() =>
            {
                // book.BookId = 0;

                _libDbContext.Books.Add(new Book{
                    BookName = book.BookName,
                    CategoryId = book.CategoryId
                });
                _libDbContext.SaveChanges();
            });
        }

        public bool delete(int id)
        {
            return _transactionManager.transactionManager(() =>
            {
                var target = _libDbContext.Books.Find(id);
                if (target == null)
                {
                    throw new Exception("Book not found");
                }
                _libDbContext.Books.Remove(target);
                _libDbContext.SaveChanges();
            });
        }

        public Book getBook(int id)
        {
            var book = _libDbContext.Books.Find(id);
            if(book == null) return new Book();
            return book;
        }

        public Category getCategory(int id)
        {
            var category = _libDbContext.Categories.Find(id);
            if(category == null) return new Category();
            return category;
        }

        public List<Book> showList()
        {
            return _libDbContext.Books.ToList();
        }

        public bool update(int id, Book book)
        {
            return _transactionManager.transactionManager(() =>
            {
                var target = _libDbContext.Books.Find(id);
                if (target == null)
                {
                    throw new Exception("Book not found");
                }
                var cateOfTarget = _libDbContext.Categories.Find(target.CategoryId);
                if(cateOfTarget == null)
                {
                    throw new Exception("Category not found");
                }
                target.BookName = book.BookName;
                target.CategoryId = book.CategoryId;
                target.UpdatedAt = DateTime.Now;
                
                _libDbContext.SaveChanges();
            });
        }
    }
}