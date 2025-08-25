using AdminPanel.Services.AttributeService;
using ChronoSekai.Shared.Contracts.AttributeService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [ApiController]
    [Route("/api/admin-publisher")]
    public class PublisherAPIController(PublisherAPIClient publisherAPIClient, ILogger<PublisherAPIController> logger) : Controller
    {
        private readonly ILogger<PublisherAPIController> _logger = logger;
        private readonly PublisherAPIClient _publisherAPIClient = publisherAPIClient;

        public async Task<ActionResult<IEnumerable<PublisherDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _publisherAPIClient.GetAllAsync();

                return Ok(result.OrderBy(x => x.Id));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
