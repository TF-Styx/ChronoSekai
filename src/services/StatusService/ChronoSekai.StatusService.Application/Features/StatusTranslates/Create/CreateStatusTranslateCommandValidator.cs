using FluentValidation;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using ChronoSekai.StatusService.Application.InterfaceRepositories;

namespace ChronoSekai.StatusService.Application.Features.StatusTranslates.Create
{
    internal class CreateStatusTranslateCommandValidator : AbstractValidator<CreateStatusTranslateCommand>
    {
        private readonly IStatusTranslateRepository _statusTranslateRepository;

        public CreateStatusTranslateCommandValidator(IStatusTranslateRepository statusTranslateRepository)
        {
            _statusTranslateRepository = statusTranslateRepository;

            Include(new NameValidation<CreateStatusTranslateCommand>(50));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _statusTranslateRepository.ExistName(name))
                .WithMessage("Данное имя уже существует!");
            });
        }
    }
}
