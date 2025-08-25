using ChronoSekai.Shared.Contracts.AttributeService;

namespace AdminPanel.Services.AttributeService
{
    public class ArtistAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<ArtistDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/artists");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<ArtistDTO>>();
        }
    }
}
