using AdminPanel.Services.AttributeService;
using ChronoSekai.Shared.Contracts.AttributeRequest.ArtistRequest;
using ChronoSekai.Shared.Contracts.AttributeRequest.AuthorRequest;
using ChronoSekai.Shared.Contracts.AttributeService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers.Attributes
{
    [ApiController]
    [Route("/api/admin-author")]
    public class AuthorAPIController(AuthorAPIClient authorAPIClient, ILogger<AuthorAPIController> logger) : Controller
    {
        private readonly ILogger<AuthorAPIController> _logger = logger;
        private readonly AuthorAPIClient _authorAPIClient = authorAPIClient;

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> CreateAsync([FromBody] CreateAuthorRequest request)
        {
            try
            {
                var result = await _authorAPIClient.CreateAsync(request);

                if (result == null)
                {
                    _logger.LogWarning("API создания атрибута вернуло пустой результат.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "API не вернуло созданный объект.");
                }

                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при создании атрибута через API. DTO: {@CreateDTO}", nameof(request));
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Сервис атрибутов временно недоступен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка в CreateAsync. DTO: {@CreateDTO}", nameof(request));
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _authorAPIClient.GetAllAsync();

                return Ok(result.OrderBy(x => x.Id));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка атрибутов из API.");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Сервис атрибутов временно недоступен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка в GetAllAsync.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateAuthorNameRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _authorAPIClient.UpdateNameAsync(id, request);
                return NoContent();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении статуса (ID: {AttributeId}) через API.", id);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Сервис статусов временно недоступен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка в UpdateAsync (ID: {AttributeId}).", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _authorAPIClient.DeleteAsync(id);

                return NoContent();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при удалении атрибута (ID: {AttributeId}) через API.", id);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Сервис атрибутов временно недоступен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка в DeleteAsync (ID: {AttributeId}).", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера.");
            }
        }
    }
}
