using ChronoSekai.AttributeService.Application.Features.Genres.Create;
using ChronoSekai.AttributeService.Application.Features.Genres.Delete;
using ChronoSekai.AttributeService.Application.Features.Genres.GetAll;
using ChronoSekai.AttributeService.Application.Features.Genres.Update;
using ChronoSekai.Shared.API;
using ChronoSekai.Shared.Contracts.AttributeRequest.GenreRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChronoSekai.AttributeService.API.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGenreRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateGenreCommand(request.Name);

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
            var command = new GetAllGenreQuery();

            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPut("{id}/update-name")]
        public async Task<IActionResult> UpdateName(int id, [FromBody] UpdateGenreNameRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateGenreNameCommand(id, request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return result.Match
                (
                    onSuccess: () => Ok("Изменение прошло успешно!"),
                    onFailure: errors => SwitchResult(errors.FirstOrDefault()!)
                );
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGenreRequest request, CancellationToken cancellationToken)
        {
            var command = new DeleteGenreCommand(request.Id);

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
