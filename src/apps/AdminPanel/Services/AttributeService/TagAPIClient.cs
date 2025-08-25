using ChronoSekai.Shared.Contracts.AttributeService;

namespace AdminPanel.Services.AttributeService
{
    public class TagAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<TagDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/tags");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<TagDTO>>();
        }
    }
}
