using WheelOfFortune.Shared.Model.Tokens;
using WheelOfFortune.Shared.Model.User;

namespace WheelOfFortune.Client.Services
{
    public interface IAuthService
    {
        Task<bool> Login(AuthenticateUserDto loginModel);
        Task<bool> Register(RegisterUserDto registerModel);
    }
}