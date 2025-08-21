using ChronoSekai.AttributeService.API.Models.Request.ArtistRequest;
using ChronoSekai.AttributeService.Application.Features.Artists.Create;
using ChronoSekai.AttributeService.Application.Features.Artists.Delete;
using ChronoSekai.AttributeService.Application.Features.Artists.Update;
using ChronoSekai.Shared.API;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoSekai.AttributeService.API.Controllers
{
    [ApiController]
    [Route("api/artists")]
    public class ArtistController(IMediator mediator) : BaseController
    {
        private IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateArtistCommand(request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok(result.Value),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }

        [HttpPut("{id}/update-name")]
        public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateArtistNameRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateArtistNameCommand(id, request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Изменение прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteArtistRequest request, CancellationToken cancellationToken)
        {
            var command = new DeleteArtistCommand(request.Id);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Удаление прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }
    }
}
