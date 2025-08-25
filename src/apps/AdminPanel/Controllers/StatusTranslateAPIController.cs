using AdminPanel.Services.StatusService;
using ChronoSekai.Shared.Contracts.StatusService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [ApiController]
    [Route("/api/admin-status-translate")]
    public class StatusTranslateAPIController(StatusTranslateAPIClient statusTranslateAPIClient, ILogger<StatusTranslateAPIController> logger) : Controller
    {
        private readonly ILogger<StatusTranslateAPIController> _logger = logger;
        private readonly StatusTranslateAPIClient _statusTranslateAPIClient = statusTranslateAPIClient;

        public async Task<ActionResult<IEnumerable<StatusTranslateDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _statusTranslateAPIClient.GetAllAsync();

                return Ok(result.OrderBy(x => x.Id));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
