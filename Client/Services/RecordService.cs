using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WheelOfFortune.Client.Providers;
using WheelOfFortune.Shared.Model.Record;
using WheelOfFortune.Shared.Model.Tokens;
using WheelOfFortune.Shared.Model.User;
namespace WheelOfFortune.Client.Services
{
    public interface IRecordService
    {
        Task<int> Create(CreateRecordDto recordDto);
        Task<bool> Delete(int recordId);
        Task<IEnumerable<ReadRecordDto>?> GetRecordsByUser();
    }

    public class RecordService : IRecordService
    {
        private readonly HttpClient _httpClient;
        public RecordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ReadRecordDto>?> GetRecordsByUser()
        {

            return await _httpClient.GetFromJsonAsync<List<ReadRecordDto>>("api/Record/GetRecordsByUser");
        }
        public async Task<bool> Delete(int recordId)
        {

            return (await _httpClient.DeleteAsync($"api/Record/Delete/{recordId}")).IsSuccessStatusCode;
        }
        public async Task<int> Create(CreateRecordDto recordDto)
        {

            var result = await _httpClient.PostAsJsonAsync($"api/Record/Create", recordDto);
            if (result.IsSuccessStatusCode)
            {
                return int.Parse(await result.Content.ReadAsStringAsync());
            }
            return 0;

        }



    }
}
