using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using FluentValidation;

namespace ChronoSekai.AttributeService.Application.Features.Genres.Update
{
    public sealed class UpdateGenreNameCommandValidator : AbstractValidator<UpdateGenreNameCommand>
    {
        private readonly IGenreRepository _genreRepository;

        public UpdateGenreNameCommandValidator(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;

            Include(new NameValidation<UpdateGenreNameCommand>(100));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _genreRepository.ExistName(name))
                .WithMessage("Данное имя уже занято!");
            });
        }
    }
}
