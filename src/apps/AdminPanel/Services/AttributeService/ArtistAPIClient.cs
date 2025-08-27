using ChronoSekai.Shared.Contracts.AttributeRequest.ArtistRequest;
using ChronoSekai.Shared.Contracts.AttributeService;

namespace AdminPanel.Services.AttributeService
{
    public class ArtistAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<ArtistDTO> CreateAsync(CreateArtistRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/artists", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ArtistDTO>();
        }

        public async Task<List<ArtistDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/artists");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ArtistDTO>>();
        }

        public async Task UpdateNameAsync(int id, UpdateArtistNameRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/artists/{id}/update-name", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var requestDto = new DeleteArtistRequest(id);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress, "api/artists"), // Базовый URL эндпоинта
                Content = JsonContent.Create(requestDto) // Сериализуем DTO в тело запроса
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
