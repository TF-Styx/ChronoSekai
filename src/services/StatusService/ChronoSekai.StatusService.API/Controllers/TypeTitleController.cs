using ChronoSekai.Shared.API;
using ChronoSekai.StatusService.API.Models.Request.TypeTitleRequest;
using ChronoSekai.StatusService.Application.Features.TypeTitles.Create;
using ChronoSekai.StatusService.Application.Features.TypeTitles.Delete;
using ChronoSekai.StatusService.Application.Features.TypeTitles.GetAll;
using ChronoSekai.StatusService.Application.Features.TypeTitles.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoSekai.StatusService.API.Controllers
{
    [ApiController]
    [Route("api/type-titles")]
    public class TypeTitleController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTypeTitleRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateTypeTitleCommand(request.Name);

            var result = await _mediator.Send(command);

            return result.Match
                (
                    onSuccess: () => Ok(result.Value),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var command = new GetAllTypeTitleQuery();

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}/update-name")]
        public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateTypeTitleNameRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateTypeTitleNameCommand(id, request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Изменение прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTypeTitleRequest request, CancellationToken cancellationToken)
        {
            var command = new DeleteTypeTitleCommand(request.Id);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Удаление прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }
    }
}
