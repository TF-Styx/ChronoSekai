using ChronoSekai.Shared.Contracts.AttributeRequest.TagRequest;
using ChronoSekai.Shared.Contracts.AttributeService;

namespace AdminPanel.Services.AttributeService
{
    public class TagAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<TagDTO> CreateAsync(CreateTagRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/tags", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TagDTO>();
        }

        public async Task<List<TagDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/tags");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<TagDTO>>();
        }

        public async Task UpdateNameAsync(int id, UpdateTagNameRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/tags/{id}/update-name", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var requestDto = new DeleteTagRequest(id);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress, "api/tags"), // Базовый URL эндпоинта
                Content = JsonContent.Create(requestDto) // Сериализуем DTO в тело запроса
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
