using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Repository
{
    public interface IUserRepository
    {
        public User verifyUser(User user);

    }
}