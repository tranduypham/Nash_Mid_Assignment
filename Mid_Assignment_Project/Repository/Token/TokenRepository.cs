using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mid_Assignment_Project.Models;

namespace Mid_Assignment_Project.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private const int EXPIRATION_DURATION = 3; // Day
        private LibaryDbContext _libDbContext;
        private TransactionManager _transactionManager;
        private List<Token> list;
        public TokenRepository(LibaryDbContext libDbContext)
        {
            _libDbContext = libDbContext;
            _transactionManager = new TransactionManager(_libDbContext);
            list = _libDbContext.Tokens.ToList();
        }
        public Boolean clearToken(string tokenPayload)
        {
            Token deleteToken = (from token in list
                                 where token.Payload == tokenPayload
                                 select token).First();
            return _transactionManager.transactionManager(() =>
            {
                _libDbContext.Tokens.Remove(deleteToken);
                _libDbContext.SaveChanges();
            });
        }

        private string genToken()
        {
            Guid payload = Guid.NewGuid();
            // Check if new pay load is replicate
            bool isReplicate = (from token in list
                                where token.Payload == payload.ToString()
                                select token).Count() > 0;
            if (isReplicate)
            {
                this.genToken();
            }
            return payload.ToString();
        }

        public Token createToken(User user)
        {
            Boolean result = false;
            Boolean isLogin = (from t in list
                                where t.UserId == user.UserId
                                select t).Count() >= 1;
            if(!isLogin){
                string payload = this.genToken();
                Token token = new Token
                {
                    TokenId = 0,
                    Payload = payload,
                    UserId = user.UserId
                };
                while (result == false)
                {
                    result = _transactionManager.transactionManager(() =>
                    {
                        _libDbContext.Tokens.Add(token);
                        _libDbContext.SaveChanges();
                    });
                }
                return token;
            }else{
                Token token = _libDbContext.Tokens.Where(t => t.UserId == user.UserId).FirstOrDefault();
                this.resetToken(token.Payload);
                return token;
            }
        }

        public Boolean resetToken(string tokenPayload)
        {
            int targetTokenId = (from token in list
                                 where token.Payload == tokenPayload
                                 select token.TokenId).First();
            return _transactionManager.transactionManager(() =>
            {
                Token targetToken = _libDbContext.Tokens.Find(targetTokenId);
                targetToken.CreatedAt = DateTime.Now;
                _libDbContext.SaveChanges();
            });
        }
        public Boolean verifyToken(string tokenPayload, bool checkAdmin)
        {
            bool result = false;
            Token targetToken = (from token in list
                                 where token.Payload == tokenPayload
                                 select token).FirstOrDefault();
            if (targetToken == null) return false;
            DateTime tokenCreateAt = targetToken.CreatedAt;
            DateTime tokenExpired = tokenCreateAt.AddDays(EXPIRATION_DURATION);
            DateTime now = DateTime.Now;
            try
            {
                if (now > tokenExpired)
                {
                    result = false;
                    this.clearToken(tokenPayload);
                }
                else
                {
                    result = true;
                    this.resetToken(tokenPayload);
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            if(checkAdmin){
                User user = _libDbContext.Users.Find(targetToken.UserId);
                if(user == null){
                    result = false;
                }else{
                    result = user.IsSuper ? true : false;
                }
            }
            return result;
        }

        public Token getToken(string tokenPayload)
        {
            return _libDbContext.Tokens.Where(t => t.Payload == tokenPayload).First();
        }
    }
}