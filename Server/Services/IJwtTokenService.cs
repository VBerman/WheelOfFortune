using WheelOfFortune.Shared.Model.Tokens;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Server.Services
{
    public interface IJwtTokenService
    {
        TokenPairDto IssueTokenPair(UserEntity user, int refreshTokenId);
        IDictionary<string, string>? ParseToken(string token);
    }
}