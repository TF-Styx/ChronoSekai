using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using FluentValidation;

namespace ChronoSekai.AttributeService.Application.Features.Artists.Update
{
    public sealed class UpdateArtistNameCommandValidator : AbstractValidator<UpdateArtistNameCommand>
    {
        private readonly IArtistRepository _artistRepository;

        public UpdateArtistNameCommandValidator(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;

            Include(new NameValidation<UpdateArtistNameCommand>(100));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _artistRepository.ExistName(name))
                .WithMessage("Данное имя уже занято!");
            });
        }
    }
}
