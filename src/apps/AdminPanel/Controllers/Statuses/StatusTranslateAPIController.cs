using AdminPanel.Services.StatusService;
using ChronoSekai.Shared.Contracts.StatusRequest.StatusTranslateRequest;
using ChronoSekai.Shared.Contracts.StatusService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers.Statuses
{
    [ApiController]
    [Route("/api/admin-status-translate")]
    public class StatusTranslateAPIController(StatusTranslateAPIClient statusTranslateAPIClient, ILogger<StatusTranslateAPIController> logger) : Controller
    {
        private readonly ILogger<StatusTranslateAPIController> _logger = logger;
        private readonly StatusTranslateAPIClient _statusTranslateAPIClient = statusTranslateAPIClient;

        [HttpPost]
        public async Task<ActionResult<StatusTranslateDTO>> CreateAsync([FromBody] CreateStatusTranslateRequest request)
        {
            try
            {
                var result = await _statusTranslateAPIClient.CreateAsync(request);

                if (result == null)
                {
                    _logger.LogWarning("API создания статуса вернуло пустой результат.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "API не вернуло созданный объект.");
                }

                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при создании статуса через API. DTO: {@CreateDTO}", nameof(request));
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Сервис статусов временно недоступен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка в CreateAsync. DTO: {@CreateDTO}", nameof(request));
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusTranslateDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _statusTranslateAPIClient.GetAllAsync();

                return Ok(result.OrderBy(x => x.Id));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка статусов из API.");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Сервис статусов временно недоступен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка в GetAllAsync.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateStatusTranslateNameRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _statusTranslateAPIClient.UpdateNameAsync(id, request);
                return NoContent();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении статуса (ID: {StatusId}) через API.", id);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Сервис статусов временно недоступен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка в UpdateAsync (ID: {StatusId}).", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _statusTranslateAPIClient.DeleteAsync(id);

                return NoContent();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при удалении статуса (ID: {StatusId}) через API.", id);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Сервис статусов временно недоступен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка в DeleteAsync (ID: {StatusId}).", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера.");
            }
        }
    }
}
