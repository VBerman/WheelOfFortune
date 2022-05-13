using System.Net.Http.Json;
using System.Text.Json;
using WheelOfFortune.Shared.Model.RealEstate;

namespace WheelOfFortune.Client.Services
{
    public class RealEstateService
    {
        private readonly HttpClient _httpClient;
        public RealEstateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ReadRealEstateDto>> Get(bool isConfirmed)
        {
            var response = await _httpClient.GetAsync($"api/RealEstate/Get?isConfirmed={isConfirmed}");
            if (!response.IsSuccessStatusCode)
            {
                return new HashSet<ReadRealEstateDto>();
            }
            var result = JsonSerializer.Deserialize<HashSet<ReadRealEstateDto>>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result is null ? new HashSet<ReadRealEstateDto>() : result;
        }

        public async Task<ReadRealEstateDto?> Get(int realEstateId)
        {
            var response = await _httpClient.GetAsync($"api/RealEstate/Get/{realEstateId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var result = JsonSerializer.Deserialize<ReadRealEstateDto>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
        public async Task<bool> Update(UpdateRealEstateDto updateRealEstateDto) 
        {
            var response = await _httpClient.PostAsJsonAsync($"api/RealEstate/Update", updateRealEstateDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateConfirmed(UpdateConfirmedRealEstateDto updateConfirmedRealEstateDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/RealEstate/UpdateConfirmed", updateConfirmedRealEstateDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<string> Create(CreateRealEstateDto createRealEstateDto)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/RealEstate/Create", createRealEstateDto);
            if (response.IsSuccessStatusCode)
            {
                var id = await response.Content.ReadAsStringAsync();
                return id;
            }
            return "Error";
        }
    }
}
