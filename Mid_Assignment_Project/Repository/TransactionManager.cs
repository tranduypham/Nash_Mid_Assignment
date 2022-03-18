using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Repository
{
    public class TransactionManager
    {
        private LibaryDbContext _libDbContext;
        public TransactionManager(LibaryDbContext libDbContext)
        {
            _libDbContext = libDbContext;
        }
        public Boolean transactionManager(Action callback)
        {
            var tranaction = _libDbContext.Database.BeginTransaction();
            try
            {
                callback();
                tranaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                tranaction.Rollback();
                return false;
                throw e;
            }
            return false;
        }
    }
}