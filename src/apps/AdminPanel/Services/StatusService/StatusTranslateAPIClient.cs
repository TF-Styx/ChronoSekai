using ChronoSekai.Shared.Contracts.StatusRequest.StatusTranslateRequest;
using ChronoSekai.Shared.Contracts.StatusService;

namespace AdminPanel.Services.StatusService
{
    public class StatusTranslateAPIClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<StatusTranslateDTO> CreateAsync(CreateStatusTranslateRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/status-translates", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<StatusTranslateDTO>();
        }

        public async Task<List<StatusTranslateDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/status-translates");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<StatusTranslateDTO>>();
        }

        public async Task UpdateNameAsync(int id, UpdateStatusTranslateNameRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/status-translates/{id}/update-name", request);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var requestDTO = new DeleteStatusTranslateRequest(id);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(_httpClient.BaseAddress, "api/status-translates"),
                Content = JsonContent.Create(requestDTO)
            };

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
