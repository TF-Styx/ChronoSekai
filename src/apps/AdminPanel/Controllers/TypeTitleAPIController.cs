using AdminPanel.Services.StatusService;
using ChronoSekai.Shared.Contracts.StatusService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [ApiController]
    [Route("/api/admin-type-title")]
    public class TypeTitleAPIController(TypeTitleAPIClient typeTitleAPIClient, ILogger<TypeTitleAPIController> logger) : Controller
    {
        private readonly ILogger<TypeTitleAPIController> _logger = logger;
        private readonly TypeTitleAPIClient _typeTitleAPIClient = typeTitleAPIClient;

        public async Task<ActionResult<IEnumerable<TypeTitleDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _typeTitleAPIClient.GetAllAsync();

                return Ok(result.OrderBy(x => x.Id));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
