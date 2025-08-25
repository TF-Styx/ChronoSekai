using ChronoSekai.Shared.Contracts.AttributeService;

namespace AdminPanel.Services.AttributeService
{
    public class PublisherAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<PublisherDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/publishers");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<PublisherDTO>>();
        }
    }
}
