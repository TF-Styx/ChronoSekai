using ChronoSekai.Shared.Contracts.AttributeRequest.GenreRequest;
using ChronoSekai.Shared.Contracts.AttributeService;

namespace AdminPanel.Services.AttributeService
{
    public class GenreAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<GenreDTO> CreateAsync(CreateGenreRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/genres", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GenreDTO>();
        }

        public async Task<List<GenreDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/genres");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<GenreDTO>>();
        }

        public async Task UpdateNameAsync(int id, UpdateGenreNameRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/genres/{id}/update-name", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var requestDto = new DeleteGenreRequest(id);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress, "api/genres"), // Базовый URL эндпоинта
                Content = JsonContent.Create(requestDto) // Сериализуем DTO в тело запроса
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
