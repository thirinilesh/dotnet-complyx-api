using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComplyX.Shared.Helper
{
    public static class Util
    {
        public static DateTime GetCurrentCSTDateAndTime()
        {
            return TimeZoneInfo.ConvertTime(
                DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")
            );
        }

        public static string ReplaceChar(this string s, char[] separators, string newVal)
        {
            var temp = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(newVal, temp);
        }

        public static string ReplaceUrlCharacters(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            return str.ReplaceChar(new[] { '&', '%', '?', '/', '=', '+' }, "$");
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                var refreshToken = Convert.ToBase64String(randomNumber);
                return refreshToken.ReplaceUrlCharacters();
            }
        }

        public static ClaimsPrincipal GetPrincipalFromExpiredToken(string key, string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

       
    }
}
