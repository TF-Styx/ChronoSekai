using AdminPanel.Services.AttributeService;
using ChronoSekai.Shared.Contracts.AttributeService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [ApiController]
    [Route("/api/admin-artist")]
    public class ArtistAPIController(ArtistAPIClient artistAPIClient, ILogger<ArtistAPIController> logger) : Controller
    {
        private readonly ILogger<ArtistAPIController> _logger = logger;
        private readonly ArtistAPIClient _artistAPIClient = artistAPIClient;

        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _artistAPIClient.GetAllAsync();

                return Ok(result.OrderBy(x => x.Id));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
