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
using WheelOfFortune.Shared.ViewModel;

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


        public async Task<IEnumerable<ChatModel>> GetChats()
        {
            var response = await _httpClient.GetAsync("api/Chat/GetChats");
            if (!response.IsSuccessStatusCode)
            {
                return new List<ChatModel>();
            }
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            options.PropertyNameCaseInsensitive = true;
            var result = JsonSerializer.Deserialize<List<ChatModel>>(await response.Content.ReadAsStringAsync(), options);
            return result ?? new List<ChatModel>();
        }

        public async Task<int> CreateChat(int realEstateId)
        {
            var response = await _httpClient.PostAsync($"api/Chat/CreateChat/{realEstateId}", null);
            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }
            return int.Parse(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<MessageModel>> GetChatMessage(int chatId)
        {
            var response = await _httpClient.GetAsync($"api/Chat/GetChatMessages/{chatId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<MessageModel>();
            }
            var options = new JsonSerializerOptions();
            options.Converters.Add(new JsonStringEnumConverter());
            options.PropertyNameCaseInsensitive = true;
            var result = JsonSerializer.Deserialize<List<MessageModel>>(await response.Content.ReadAsStringAsync(), options);
            return result ?? new List<MessageModel>();
        }
    }
}
