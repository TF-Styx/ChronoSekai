using ChronoSekai.Shared.API.Application.Validations.Realization;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using FluentValidation;

namespace ChronoSekai.StatusService.Application.Features.TypeTitles.Update
{
    internal class UpdateTypeTitleNameCommandValidator : AbstractValidator<UpdateTypeTitleNameCommand>
    {
        private readonly ITypeTitleRepository _typeTitleRepository;

        public UpdateTypeTitleNameCommandValidator(ITypeTitleRepository typeTitleRepository)
        {
            _typeTitleRepository = typeTitleRepository;

            Include(new NameValidation<UpdateTypeTitleNameCommand>(50));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _typeTitleRepository.ExistName(name))
                .WithMessage("Данное имя уже занято!");
            });
        }
    }
}
