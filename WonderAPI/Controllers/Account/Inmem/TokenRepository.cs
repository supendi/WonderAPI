using System.Collections.Generic;
using System.Linq;
using WonderAPI.Entities;

namespace WonderAPI.Controllers.Account.Inmem
{
    /// <summary>
    /// In memory token repository which use memory as storage.
    /// </summary>
    public class TokenRepository : ITokenRepository
    {
        public List<Token> Tokens { get; set; }

        /// <summary>
        /// Create new inmem instance with initial data
        /// </summary>
        /// <param name="tokens"></param>
        public TokenRepository(List<Token> tokens)
        {
            this.Tokens = tokens;
        }

        /// <summary>
        /// Get last ID, or current ID. Will return the biggest number
        /// </summary>
        /// <returns></returns>
        private int GetLastID()
        {
            if (Tokens == null || Tokens.Count == 0)
            {
                return 0;
            }
            return Tokens.OrderByDescending(x => x.ID).FirstOrDefault().ID;
        }

        /// <summary>
        /// Get next auto ID
        /// </summary>
        /// <returns></returns>
        private int GetNextID()
        {
            return GetLastID() + 1;
        }


        /// <summary>
        /// Add new token into memory
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Token Add(Token token)
        {
            token.ID = GetNextID();
            this.Tokens.Add(token);
            return token;
        }

        /// <summary>
        /// Get token by its email
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public Token GetByRefreshToken(string refreshToken)
        {
            if (Tokens == null || Tokens.Count == 0)
            {
                return null;
            }
            return Tokens.FirstOrDefault(x => x.RefreshToken == refreshToken);
        }

        /// <summary>
        /// Get token by its ID
        /// </summary>
        /// <param name="tokenID"></param>
        /// <returns></returns>
        public Token GetById(int tokenID)
        {
            if (Tokens == null || Tokens.Count == 0)
            {
                return null;
            }
            return Tokens.FirstOrDefault(x => x.ID == tokenID);
        }

        /// <summary>
        /// Blacklist existing token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Token BlackList(int tokenID)
        {
            foreach (var element in Tokens)
            {
                if (element.ID == tokenID)
                {
                    element.BlackListed = true;
                    return element;
                }
            }
            return null;
        }

        /// <summary>
        /// Blacklist existing token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public void Delete(int tokenID)
        {
            Token token = null;
            foreach (var element in Tokens)
            {
                if (element.ID == tokenID)
                {
                    element.BlackListed = true;
                    token = element;
                    break;
                }
            }
            if (token != null)
                Tokens.Remove(token);
        }

        /// <summary>
        /// Dispose data
        /// </summary>
        public void Dispose()
        {
            this.Tokens = null;
        }
    }
}
