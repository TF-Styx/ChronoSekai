using AdminPanel.Services.StatusService;
using ChronoSekai.Shared.Contracts.StatusRequest.TypeTitleRequest;
using ChronoSekai.Shared.Contracts.StatusService;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers.Statuses
{
    [ApiController]
    [Route("/api/admin-type-title")]
    public class TypeTitleAPIController(TypeTitleAPIClient typeTitleAPIClient, ILogger<TypeTitleAPIController> logger) : Controller
    {
        private readonly ILogger<TypeTitleAPIController> _logger = logger;
        private readonly TypeTitleAPIClient _typeTitleAPIClient = typeTitleAPIClient;

        [HttpPost]
        public async Task<ActionResult<TypeTitleDTO>> CreateAsync([FromBody] CreateTypeTitleRequest request)
        {
            try
            {
                var result = await _typeTitleAPIClient.CreateAsync(request);

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

        public async Task<ActionResult<IEnumerable<TypeTitleDTO>>> GetAllAsync()
        {
            try
            {
                var result = await _typeTitleAPIClient.GetAllAsync();

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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateTypeTitleNameRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _typeTitleAPIClient.UpdateNameAsync(id, request);
                return NoContent();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении статуса (ID: {TypeId}) через API.", id);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Сервис статусов временно недоступен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка в UpdateAsync (ID: {TypeId}).", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _typeTitleAPIClient.DeleteAsync(id);

                return NoContent();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Ошибка при удалении статуса (ID: {TypeId}) через API.", id);
                return StatusCode(StatusCodes.Status503ServiceUnavailable, "Сервис статусов временно недоступен.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка в DeleteAsync (ID: {TypeId}).", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла внутренняя ошибка сервера.");
            }
        }
    }
}
