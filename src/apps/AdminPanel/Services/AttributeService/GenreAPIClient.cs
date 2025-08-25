using ChronoSekai.Shared.Contracts.AttributeService;

namespace AdminPanel.Services.AttributeService
{
    public class GenreAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<GenreDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/genres");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<GenreDTO>>();
        }
    }
}
