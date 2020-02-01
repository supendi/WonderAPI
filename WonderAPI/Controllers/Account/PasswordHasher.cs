namespace WonderAPI.Controllers.Account
{
    /// <summary>
    /// Interface to hash a password
    /// </summary>
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hashedPassword);
    }

    /// <summary>
    /// Implements IPasswordHasher and it use Bcrypt Algorithm
    /// </summary>
    public class BCryptHasher : IPasswordHasher
    {
        public string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
