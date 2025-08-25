using ChronoSekai.Shared.Contracts.StatusService;

namespace AdminPanel.Services.StatusService
{
    public class StatusTitleAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<StatusTitleDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/status-titles");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<StatusTitleDTO>>();
        }
    }
}
