using System.Net.Http.Json;
using WheelOfFortune.Shared.Model.Rent;

namespace WheelOfFortune.Client.Services
{
    public interface IRentService
    {
        Task<bool> Add(CreateRentDto createRentDto);
        Task<List<ReadRentDto>> GetByLandlord();
        Task<List<ReadRentDto>> GetByRealEstate(int realEstateId);
        Task<bool> SetDebt(int rentId);
    }
    public class RentService : IRentService
    {
        private readonly HttpClient _httpClient;
        public RentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Add(CreateRentDto createRentDto)
        {
            return (await _httpClient.PostAsJsonAsync("api/Rent/Add", createRentDto)).IsSuccessStatusCode;
        }

        public async Task<List<ReadRentDto>> GetByRealEstate(int realEstateId)
        {
            return await _httpClient.GetFromJsonAsync<List<ReadRentDto>>($"api/Rent/GetByRealEstate/{realEstateId}");
        }

        public async Task<List<ReadRentDto>> GetByLandlord()
        {
            return await _httpClient.GetFromJsonAsync<List<ReadRentDto>>($"api/Rent/GetByLandlord/");
        }

        public async Task<bool> SetDebt(int rentId)
        {
            return (await _httpClient.GetAsync($"api/Rent/SetDebt/{rentId}")).IsSuccessStatusCode;
        }


    }
}
