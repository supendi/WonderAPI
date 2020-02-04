using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WonderAPI.Entities;

namespace WonderAPI.Controllers.Account.Auth
{
    public class TokenRepository : ITokenRepository
    {
        private WonderDBContext db;

        public TokenRepository(WonderDBContext db)
        {
            this.db = db;
        }

        public Token Add(Token token)
        {
            db.Token.Add(token);
            db.SaveChanges();
            return token;
        }

        public Token BlackList(int tokenID)
        {
            var token = db.Token.Find(tokenID);
            if (token != null)
            {
                token.BlackListed = true;
                db.SaveChanges();
            }
            return token;
        }

        public void Delete(int tokenID)
        {
            var token = db.Token.Find(tokenID);
            db.Token.Remove(token);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Token GetByRefreshToken(string refreshToken)
        {
            var token = db.Token.FirstOrDefault(x => x.RefreshToken == refreshToken);
            return token;
        }
    }
}
