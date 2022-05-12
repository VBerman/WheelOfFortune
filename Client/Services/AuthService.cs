using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using WheelOfFortune.Client.Providers;
using WheelOfFortune.Shared.Model.Tokens;
using WheelOfFortune.Shared.Model.User;
namespace WheelOfFortune.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        public AuthService(HttpClient httpClient,
                       AuthenticationStateProvider authenticationStateProvider,
                       ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        //public async Task<ReadUserDto> Register(RegisterUserDto registerUserDto)
        //{


        //}
        public async Task<bool> Login(AuthenticateUserDto loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/Authenticate", loginModel);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var loginResult = JsonSerializer.Deserialize<TokenPairDto>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            await _localStorage.SetItemAsync("accessToken", loginResult.AccessToken);
            await _localStorage.SetItemAsync("refreshToken", loginResult.RefreshToken);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResult.AccessToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

            return true;
        }

        public async Task<bool> Register(RegisterUserDto registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/Register", registerModel);
            return response.IsSuccessStatusCode;

        }
    }
}
