using ChronoSekai.Shared.Contracts.StatusService;

namespace AdminPanel.Services.StatusService
{
    public class StatusTranslateAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<StatusTranslateDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/status-translates");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<StatusTranslateDTO>>();
        }
    }
}
