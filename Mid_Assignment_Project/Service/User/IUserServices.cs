using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Service
{
    public interface IUserServices
    {
        public Token Login(User user);
        public Boolean Logout(string token);
        public Boolean IsAdmin(string token);
        public Boolean IsValid(string token);
        public int getUserId(string token);
        public List<BookBorrowingRequest> listUserBookRequest(string token);
    }
}