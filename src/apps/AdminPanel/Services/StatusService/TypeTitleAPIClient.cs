using ChronoSekai.Shared.Contracts.StatusRequest.TypeTitleRequest;
using ChronoSekai.Shared.Contracts.StatusService;

namespace AdminPanel.Services.StatusService
{
    public class TypeTitleAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<TypeTitleDTO> CreateAsync(CreateTypeTitleRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/type-titles", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TypeTitleDTO>();
        }

        public async Task<List<TypeTitleDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/type-titles");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TypeTitleDTO>>();
        }

        public async Task UpdateNameAsync(int id, UpdateTypeTitleNameRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/type-titles/{id}/update-name", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var requestDto = new DeleteTypeTitleRequest(id);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress, "api/type-titles"), // Базовый URL эндпоинта
                Content = JsonContent.Create(requestDto) // Сериализуем DTO в тело запроса
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
