using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;
using Mid_Assignment_Project.Repository;

namespace Mid_Assignment_Project.Service
{
    public class UserServices : IUserServices
    {
        private IUserRepository _userRepository;
        private ITokenRepository _tokenRepository;
        private IBookBorrowingRequestRepository _bookBorrowingRequestRepository;
        public UserServices(IUserRepository userRepository, ITokenRepository tokenRepository, IBookBorrowingRequestRepository bookBorrowingRequestRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _bookBorrowingRequestRepository = bookBorrowingRequestRepository;
        }

        public int getUserId(string token)
        {
            int userId = 0;
            bool result = _tokenRepository.verifyToken(token);
            if(result) {
                _tokenRepository.resetToken(token);
                userId = _tokenRepository.getToken(token).UserId;
            }
            return userId;
        }

        public List<BookBorrowingRequest> listUserBookRequest(string token)
        {
            bool result = _tokenRepository.verifyToken(token);
            if(result) {
                _tokenRepository.resetToken(token);
                int userId = getUserId(token);
                return _bookBorrowingRequestRepository.showUserListRequest(userId);
            }
            return new List<BookBorrowingRequest>();
        }

        public bool IsAdmin(string token)
        {
            bool result = _tokenRepository.verifyToken(token, true);
            if(result) {
                _tokenRepository.resetToken(token);
            }
            return result;
        }

        public Token? Login(User user)
        {
            User loginUser = _userRepository.verifyUser(user);
            if(loginUser != null){
                return _tokenRepository.createToken(loginUser);
            }
            return null;
        }

        public bool Logout(string token)
        {
            return _tokenRepository.clearToken(token);
        }

        public bool IsValid(string token)
        {
            bool result = _tokenRepository.verifyToken(token);
            if(result) {
                _tokenRepository.resetToken(token);
            }
            return result;
        }
    }
}