using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WheelOfFortune.Client.Providers;
using WheelOfFortune.Shared.Model.Tokens;
using WheelOfFortune.Shared.Model.User;
namespace WheelOfFortune.Client.Services
{

    public class ChatService
    {
        private readonly HttpClient _httpClient;
        private HubConnection _hubConnection;
        private readonly NavigationManager _navigationManager;
        public ChatService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        
    }
}
