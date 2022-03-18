using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Repository;
using Mid_Assignment_Project.Helper;

namespace Mid_Assignment_Project.Service
{
    public class CategoryServices : ICategoryServices
    {
        private ICategoryRepository _cateRepository;
        private IBookRepository _bookRepo;
        public CategoryServices(ICategoryRepository cateRepository, IBookRepository bookRepo)
        {
            _cateRepository = cateRepository;
            _bookRepo = bookRepo;
        }
        public bool create(Category cate)
        {
            return _cateRepository.create(cate);
        }

        public bool delete(int id)
        {
            return _cateRepository.delete(id);
        }

        public PaginateList<BookWithCategory> showBook(int cateId, CategoryPaginateParameter catePage)
        {
            List<BookWithCategory> resultList = new List<BookWithCategory>();
            List<Book> list = _cateRepository.showBook(cateId);
            foreach(Book book in list){
                var cate = _bookRepo.getCategory(cateId);
                var item = new BookWithCategory{
                    BookItem = book,
                    CategoryOfBook = cate
                };
                resultList.Add(item);
            }
            PaginateList<BookWithCategory> paginateList = PaginateList<BookWithCategory>.paginated(resultList, catePage.CurrentPage, catePage.PageSize);
            return paginateList;
        }

        public Category showCategory(int cateId)
        {
            return _cateRepository.showCategory(cateId);
        }

        public PaginateList<Category> showList(CategoryPaginateParameter catePage)
        {
            List<Category> list = _cateRepository.showList();
            PaginateList<Category> paginateList = PaginateList<Category>.paginated(list, catePage.CurrentPage, catePage.PageSize);
            return paginateList;
        }

        public bool update(int id, Category cate)
        {
            return _cateRepository.update(id, cate);
        }
    }
}