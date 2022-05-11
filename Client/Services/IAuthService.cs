using WheelOfFortune.Shared.Model.Tokens;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Client.Services
{
    public interface IAuthService
    {
        Task<TokenPairDto> Login(AuthenticateUserDto loginModel);
    }
}