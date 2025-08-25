using AdminPanel.Services.StatusService;
using ChronoSekai.Shared.Contracts.StatusService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [ApiController]
    [Route("/api/admin-status-title")]
    public class StatusTitleAPIController(StatusTitleAPIClient statusTitleAPIClient, ILogger<StatusTitleAPIController> logger) : Controller
    {
        private readonly ILogger<StatusTitleAPIController> _logger = logger;
        private readonly StatusTitleAPIClient _statusTitleAPIClient = statusTitleAPIClient;

        public async Task<ActionResult<IEnumerable<StatusTitleDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _statusTitleAPIClient.GetAllAsync();

                return Ok(result.OrderBy(x => x.Id));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
