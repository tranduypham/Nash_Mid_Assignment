using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private LibaryDbContext _libDbContext;
        private TransactionManager _transactionManager;
        public CategoryRepository(LibaryDbContext libDbContext)
        {
            _libDbContext = libDbContext;
            _transactionManager = new TransactionManager(_libDbContext);
        }
        public bool create(Category cate)
        {

            return _transactionManager.transactionManager(() =>
            {
                cate.CategoryId = 0;
                _libDbContext.Categories.Add(cate);
                _libDbContext.SaveChanges();
            });
        }

        public bool delete(int id)
        {
            return _transactionManager.transactionManager(() =>
            {
                var target = _libDbContext.Categories.Find(id);
                if (target == null)
                {
                    throw new Exception("Category not found");
                }
                _libDbContext.Categories.Remove(target);
                _libDbContext.SaveChanges();
            });
        }

        public List<Book> showBook(int cateId)
        {
            var category = _libDbContext.Categories.Find(cateId);
            if(category == null) return new List<Book>();
            return _libDbContext.Books.Where(b => b.CategoryId == cateId).ToList();
        }

        public Category showCategory(int cateId)
        {
            var category = _libDbContext.Categories.Find(cateId);
            if(category == null) return new Category();
            return category;
        }

        public List<Category> showList()
        {
            return _libDbContext.Categories.ToList();
        }

        public bool update(int id, Category cate)
        {
            return _transactionManager.transactionManager(() =>
            {
                var target = _libDbContext.Categories.Find(id);
                if (target == null)
                {
                    throw new Exception("Category not found");
                }
                target.CategoryName = cate.CategoryName;
                target.UpdatedAt = DateTime.Now;
                
                _libDbContext.SaveChanges();
            });
        }
    }
}