using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WheelOfFortune.Shared.Model.Tokens;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Server.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly TimeSpan _accessTokenLifetime;
        private readonly TimeSpan _refreshTokenLifetime;


        public JwtTokenService(IConfigurationRoot configuration)
        {
            _tokenHandler = new JwtSecurityTokenHandler();
            _tokenHandler.InboundClaimTypeMap.Clear();
            _tokenHandler.OutboundClaimTypeMap.Clear();

            var jwtSecret = Encoding.ASCII.GetBytes(configuration["JwtAuth:Secret"]);
            _securityKey = new SymmetricSecurityKey(jwtSecret);

            var accessTokenLifetimeInMinutes = int.Parse(configuration["JwtAuth:AccessTokenLifetime"]);
            _accessTokenLifetime = TimeSpan.FromMinutes(accessTokenLifetimeInMinutes);

            var refreshTokenLifetimeInDays = int.Parse(configuration["JwtAuth:RefreshTokenLifetime"]);
            _refreshTokenLifetime = TimeSpan.FromMinutes(refreshTokenLifetimeInDays);
        }

        public IDictionary<string, string>? ParseToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                RequireSignedTokens = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _securityKey,
                ValidateAudience = false,
                ValidateIssuer = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var claimsPrincipal = _tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
                return claimsPrincipal.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TokenPairDto IssueTokenPair(UserEntity user, int refreshTokenId)
        {
            var dictionaryClaims = new Dictionary<string, object>()
            {
                {"http://schemas.microsoft.com/ws/2008/06/identity/claims/role", user.Role.ToString() },
                {"http://schemas.microsoft.com/ws/2008/06/identity/claims/name", user.FullName },
                {"Email", user.Email },
                {"Sub", user.Id.ToString()}
            };

            var accessToken = IssueToken(dictionaryClaims, _accessTokenLifetime);

            dictionaryClaims.Add("Jti", refreshTokenId.ToString());

            var refreshToken = IssueToken(dictionaryClaims, _refreshTokenLifetime);

            return new TokenPairDto(accessToken, refreshToken);
        }

        private string IssueToken(IDictionary<string, object> claims, TimeSpan lifetime)
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Expires = DateTime.UtcNow.Add(lifetime),
                SigningCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenObject = _tokenHandler.CreateToken(descriptor);
            var encodedToken = _tokenHandler.WriteToken(tokenObject);

            return encodedToken;
        }
    }

}
