using System;

namespace WonderAPI.Pkg
{
    /// <summary>
    /// Interface to hash a password
    /// </summary>
    public interface IPasswordHasher
    {
        string Hash(string password);
    }

    /// <summary>
    /// Implements IPasswordHasher
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            throw new NotImplementedException();
        }
    }
}
