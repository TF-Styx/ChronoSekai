using AdminPanel.Services.AttributeService;
using ChronoSekai.Shared.Contracts.AttributeService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [ApiController]
    [Route("/api/admin-genre")]
    public class GenreAPIController(GenreAPIClient genreAPIClient, ILogger<GenreAPIController> logger) : Controller
    {
        private readonly ILogger<GenreAPIController> _logger = logger;
        private readonly GenreAPIClient _genreAPIClient = genreAPIClient;

        public async Task<ActionResult<IEnumerable<GenreDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _genreAPIClient.GetAllAsync();

                return Ok(result.OrderBy(x => x.Id));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
