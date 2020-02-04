using System;

namespace WonderAPI.Entities
{
    /// <summary>
    /// Represent token model
    /// </summary>
    public class Token
    {
        public int ID { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public bool BlackListed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
