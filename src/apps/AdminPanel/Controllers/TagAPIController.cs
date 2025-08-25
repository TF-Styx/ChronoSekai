using AdminPanel.Services.AttributeService;
using ChronoSekai.Shared.Contracts.AttributeService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [ApiController]
    [Route("/api/admin-tag")]
    public class TagAPIController(TagAPIClient tagAPIClient, ILogger<TagAPIController> logger) : Controller
    {
        private readonly ILogger<TagAPIController> _logger = logger;
        private readonly TagAPIClient _tagAPIClient = tagAPIClient;

        public async Task<ActionResult<IEnumerable<TagDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _tagAPIClient.GetAllAsync();

                return Ok(result.OrderBy(x => x.Id));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
