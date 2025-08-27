using ChronoSekai.Shared.Contracts.StatusRequest.StatusTitleRequest;
using ChronoSekai.Shared.Contracts.StatusService;

namespace AdminPanel.Services.StatusService
{
    public class StatusTitleAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<StatusTitleDTO> CreateAsync(CreateStatusTitleRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/status-titles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StatusTitleDTO>();
        }

        public async Task<List<StatusTitleDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/status-titles");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<StatusTitleDTO>>();
        }

        public async Task UpdateNameAsync(int id, UpdateStatusTitleNameRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/status-titles/{id}/update-name", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var requestDto = new DeleteStatusTitleRequest(id);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress, "api/status-titles"), // Базовый URL эндпоинта
                Content = JsonContent.Create(requestDto) // Сериализуем DTO в тело запроса
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
