using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Repository
{
    public interface ITokenRepository
    {
        public Token? createToken(User user);
        public Boolean verifyToken(string tokenPayload, bool checkAdmin =  false);
        public Boolean clearToken(string tokenPayload);
        public Boolean resetToken(string tokenPayload);
        public Token getToken(string tokenPayload);
    }
}