using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using FluentValidation;

namespace ChronoSekai.AttributeService.Application.Features.Artists.Create
{
    public sealed class CreateArtistCommandValidator : AbstractValidator<CreateArtistCommand>
    {
        private readonly IArtistRepository _artistRepository;

        public CreateArtistCommandValidator(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;

            Include(new NameValidation<CreateArtistCommand>(100));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _artistRepository.ExistName(name))
                .WithMessage("Данное имя уже существует!");
            });
        }
    }
}
