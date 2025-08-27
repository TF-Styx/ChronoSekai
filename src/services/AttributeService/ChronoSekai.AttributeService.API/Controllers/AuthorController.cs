using ChronoSekai.AttributeService.Application.Features.Authors.Create;
using ChronoSekai.AttributeService.Application.Features.Authors.Delete;
using ChronoSekai.AttributeService.Application.Features.Authors.GetAll;
using ChronoSekai.AttributeService.Application.Features.Authors.Update;
using ChronoSekai.AttributeService.Application.Features.Genres.GetAll;
using ChronoSekai.Shared.API;
using ChronoSekai.Shared.Contracts.AttributeRequest.AuthorRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoSekai.AttributeService.API.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAuthorRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateAuthorCommand(request.Name);

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
            var command = new GetAllAuthorQuery();

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}/update-name")]
        public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateAuthorNameRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateAuthorNameCommand(id, request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Изменение прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteAuthorRequest request, CancellationToken cancellationToken)
        {
            var command = new DeleteAuthorCommand(request.Id);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Удаление прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }
    }
}
