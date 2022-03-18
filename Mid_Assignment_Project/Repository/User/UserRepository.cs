using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Repository
{
    public class UserRepository : IUserRepository
    {
        private LibaryDbContext _libDbContext;
        public UserRepository(LibaryDbContext libDbContext)
        {
            _libDbContext = libDbContext;
        }
        public User? verifyUser(User user)
        {
            User? loginUser = _libDbContext.Users.Where(u => u.Username == user.Username)
                                                .Where(u => u.Password == user.Password)
                                                .FirstOrDefault();
            return loginUser;
            
        }
    }
}