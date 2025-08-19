using FluentValidation;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using ChronoSekai.StatusService.Application.InterfaceRepositories;

namespace ChronoSekai.StatusService.Application.Features.StatusTitles.Create
{
    public sealed class CreateStatusTitleCommandValidator : AbstractValidator<CreateStatusTitleCommand>
    {
        private readonly IStatusTitleRepository _statusTitleRepository;

        public CreateStatusTitleCommandValidator(IStatusTitleRepository statusTitleRepository)
        {
            _statusTitleRepository = statusTitleRepository;

            Include(new NameValidation<CreateStatusTitleCommand>(50));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _statusTitleRepository.ExistName(name))
                .WithMessage("Данное имя уже существует!");
            });
        }
    }
}
