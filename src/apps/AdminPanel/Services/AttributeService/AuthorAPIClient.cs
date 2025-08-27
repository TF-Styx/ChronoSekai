using ChronoSekai.Shared.Contracts.AttributeRequest.AuthorRequest;
using ChronoSekai.Shared.Contracts.AttributeService;

namespace AdminPanel.Services.AttributeService
{
    public class AuthorAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<AuthorDTO> CreateAsync(CreateAuthorRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/authors", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AuthorDTO>();
        }

        public async Task<List<AuthorDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/authors");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<AuthorDTO>>();
        }

        public async Task UpdateNameAsync(int id, UpdateAuthorNameRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/authors/{id}/update-name", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var requestDto = new DeleteAuthorRequest(id);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress, "api/authors"), // Базовый URL эндпоинта
                Content = JsonContent.Create(requestDto) // Сериализуем DTO в тело запроса
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
