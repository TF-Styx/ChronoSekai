using ChronoSekai.Shared.API;
using ChronoSekai.StatusService.API.Models.Request.StatusTitleRequest;
using ChronoSekai.StatusService.API.Models.Request.TypeTitleRequest;
using ChronoSekai.StatusService.Application.Features.StatusTitles.Create;
using ChronoSekai.StatusService.Application.Features.StatusTitles.Delete;
using ChronoSekai.StatusService.Application.Features.StatusTitles.GetAll;
using ChronoSekai.StatusService.Application.Features.StatusTitles.Update;
using ChronoSekai.StatusService.Application.Features.TypeTitles.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoSekai.StatusService.API.Controllers
{
    [ApiController]
    [Route("api/status-titles")]
    public class StatusTitleController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStatusTitleRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateStatusTitleCommand(request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok(result.Value),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var command = new GetAllStatusTitleQuery();

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}/update-name")]
        public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateStatusTitleNameRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateStatusTitleNameCommand(id, request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Изменение прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteStatusTitleRequest request, CancellationToken cancellationToken)
        {
            var command = new DeleteStatusTitleCommand(request.Id);

            var result = await _mediator.Send(command, cancellationToken);

            // TODO : При удаление сделать запрос к другой базе, и при удалении ставить другой статус (отсутствует)
            return result.Match
                (
                    onSuccess: () => Ok("Удаление прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }
    }
}
