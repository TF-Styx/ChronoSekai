using ChronoSekai.Shared.API;
using ChronoSekai.Shared.Contracts.StatusRequest.StatusTranslateRequest;
using ChronoSekai.StatusService.Application.Features.StatusTranslates.Create;
using ChronoSekai.StatusService.Application.Features.StatusTranslates.Delete;
using ChronoSekai.StatusService.Application.Features.StatusTranslates.GetAll;
using ChronoSekai.StatusService.Application.Features.StatusTranslates.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoSekai.StatusService.API.Controllers
{
    [ApiController]
    [Route("api/status-translates")]
    public class StatusTranslateController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStatusTranslateRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateStatusTranslateCommand(request.Name);

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
            var command = new GetAllStatusTranslateQuery();

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}/update-name")]
        public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateStatusTranslateNameRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateStatusTranslateNameCommand(id, request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Изменение прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteStatusTranslateRequest request, CancellationToken cancellationToken)
        {
            var command = new DeleteStatusTranslateCommand(request.Id);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
            (
                onSuccess: () => Ok("Удаление прошло успешно!"),
                onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
            );
        }
    }
}
