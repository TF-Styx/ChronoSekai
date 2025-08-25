using ChronoSekai.Shared.Contracts.AttributeService;

namespace AdminPanel.Services.AttributeService
{
    public class AuthorAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<AuthorDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/authors");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<AuthorDTO>>();
        }
    }
}
