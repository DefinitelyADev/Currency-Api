using System;

namespace CurrencyApi.Domain.Entities
{
    public class RefreshToken
    {
        public RefreshToken(string jwtId, string userId, DateTime dateStart, DateTime dateEnd)
        {
            JwtId = jwtId;
            DateStart = dateStart;
            DateEnd = dateEnd;
            UserId = userId;
        }

        public RefreshToken(string token, string jwtId, DateTime dateStart, DateTime dateEnd, bool used, bool invalidated, string userId)
        {
            Token = token;
            JwtId = jwtId;
            DateStart = dateStart;
            DateEnd = dateEnd;
            Used = used;
            Invalidated = invalidated;
            UserId = userId;
        }

        public string Token { get; set; }
        public string JwtId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }
        public string UserId { get; set; }
    }
}
