using ChronoSekai.AttributeService.API.Models.Request.TagRequest;
using ChronoSekai.AttributeService.Application.Features.Tags.Create;
using ChronoSekai.AttributeService.Application.Features.Tags.Delete;
using ChronoSekai.AttributeService.Application.Features.Tags.GetAll;
using ChronoSekai.AttributeService.Application.Features.Tags.Update;
using ChronoSekai.Shared.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoSekai.AttributeService.API.Controllers
{
    [ApiController]
    [Route("api/tags")]
    public class TagController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateTagCommand(request.Name);

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
            var command = new GetAllTagQuery();

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}/update-name")]
        public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateTagNameRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateTagNameCommand(id, request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Изменение прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTagRequest request, CancellationToken cancellationToken)
        {
            var command = new DeleteTagCommand(request.Id);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Удаление прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }
    }
}
