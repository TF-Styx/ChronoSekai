using ChronoSekai.AttributeService.API.Models.Request.PublisherRequest;
using ChronoSekai.AttributeService.Application.Features.Genres.GetAll;
using ChronoSekai.AttributeService.Application.Features.Publishers.Create;
using ChronoSekai.AttributeService.Application.Features.Publishers.Delete;
using ChronoSekai.AttributeService.Application.Features.Publishers.GetAll;
using ChronoSekai.AttributeService.Application.Features.Publishers.Update;
using ChronoSekai.Shared.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoSekai.AttributeService.API.Controllers
{
    [ApiController]
    [Route("api/publishers")]
    public class PublisherController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePublisherRequest request, CancellationToken cancellationToken)
        {
            var command = new CreatePublisherCommand(request.Name);

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
            var command = new GetAllPublisherQuery();

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}/update-name")]
        public async Task<IActionResult> UpdateName(int id, [FromBody] UpdatePublisherNameRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdatePublisherNameCommand(id, request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Изменение прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeletePublisherRequest request, CancellationToken cancellationToken)
        {
            var command = new DeletePublisherCommand(request.Id);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Удаление прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }
    }
}
