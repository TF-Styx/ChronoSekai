using AdminPanel.Services.AttributeService;
using ChronoSekai.Shared.Contracts.AttributeRequest.TagRequest;
using ChronoSekai.Shared.Contracts.AttributeService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers.Attributes
{
    [ApiController]
    [Route("/api/admin-tag")]
    public class TagAPIController(TagAPIClient tagAPIClient, ILogger<TagAPIController> logger) : Controller
    {
        private readonly ILogger<TagAPIController> _logger = logger;
        private readonly TagAPIClient _tagAPIClient = tagAPIClient;

        [HttpPost]
        public async Task<ActionResult<TagDTO>> CreateAsync([FromBody] CreateTagRequest request)
        {
            try
            {
                var result = await _tagAPIClient.CreateAsync(request);

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
        public async Task<ActionResult<IEnumerable<TagDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _tagAPIClient.GetAllAsync();

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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTagNameRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _tagAPIClient.UpdateNameAsync(id, request);
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
                await _tagAPIClient.DeleteAsync(id);

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
