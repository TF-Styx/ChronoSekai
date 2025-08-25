using ChronoSekai.Shared.Contracts.StatusService;

namespace AdminPanel.Services.StatusService
{
    public class TypeTitleAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<TypeTitleDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/type-titles");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<TypeTitleDTO>>();
        }
    }
}
