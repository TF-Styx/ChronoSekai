using ChronoSekai.Shared.Contracts.AttributeRequest.PublisherRequest;
using ChronoSekai.Shared.Contracts.AttributeService;

namespace AdminPanel.Services.AttributeService
{
    public class PublisherAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<PublisherDTO> CreateAsync(CreatePublisherRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/publishers", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PublisherDTO>();
        }

        public async Task<List<PublisherDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/publishers");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<PublisherDTO>>();
        }

        public async Task UpdateNameAsync(int id, UpdatePublisherNameRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/publishers/{id}/update-name", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var requestDto = new DeletePublisherRequest(id);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress, "api/publishers"), // Базовый URL эндпоинта
                Content = JsonContent.Create(requestDto) // Сериализуем DTO в тело запроса
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
