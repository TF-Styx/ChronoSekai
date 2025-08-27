using AdminPanel.Services.StatusService;
using ChronoSekai.Shared.Contracts.StatusRequest.StatusTitleRequest;
using ChronoSekai.Shared.Contracts.StatusService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers.Statuses
{
    [ApiController]
    [Route("/api/admin-status-title")]
    public class StatusTitleAPIController(StatusTitleAPIClient statusTitleAPIClient, ILogger<StatusTitleAPIController> logger) : Controller
    {
        private readonly ILogger<StatusTitleAPIController> _logger = logger;
        private readonly StatusTitleAPIClient _statusTitleAPIClient = statusTitleAPIClient;

        [HttpPost]
        public async Task<ActionResult<StatusTitleDTO>> CreateAsync([FromBody] CreateStatusTitleRequest request)
        {
            try
            {
                var result = await _statusTitleAPIClient.CreateAsync(request);

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
        public async Task<ActionResult<IEnumerable<StatusTitleDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _statusTitleAPIClient.GetAllAsync();

                return Ok(result.OrderBy(x => x.Id));
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка статусов из API.");
                // Возвращаем клиенту осмысленную ошибку
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Сервис статусов временно недоступен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка в GetAllAsync.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера.");
            }
        }

        // PUT: /api/admin-status-title/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateStatusTitleNameRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _statusTitleAPIClient.UpdateNameAsync(id, request);

                // 204 NoContent - стандартный успешный ответ для PUT/DELETE, который не возвращает тело.
                // Можно также вернуть Ok().
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
                await _statusTitleAPIClient.DeleteAsync(id);

                return NoContent(); // Успешное удаление
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
