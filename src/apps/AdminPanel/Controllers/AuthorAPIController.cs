using AdminPanel.Services.AttributeService;
using ChronoSekai.Shared.Contracts.AttributeService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [ApiController]
    [Route("/api/admin-author")]
    public class AuthorAPIController(AuthorAPIClient authorAPIClient, ILogger<AuthorAPIController> logger) : Controller
    {
        private readonly ILogger<AuthorAPIController> _logger = logger;
        private readonly AuthorAPIClient _authorAPIClient = authorAPIClient;

        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _authorAPIClient.GetAllAsync();

                return Ok(result.OrderBy(x => x.Id));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
