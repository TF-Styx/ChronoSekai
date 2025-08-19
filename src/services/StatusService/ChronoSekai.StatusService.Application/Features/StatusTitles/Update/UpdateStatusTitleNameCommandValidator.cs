using ChronoSekai.Shared.API.Application.Validations.Realization;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using FluentValidation;

namespace ChronoSekai.StatusService.Application.Features.StatusTitles.Update
{
    internal class UpdateStatusTitleNameCommandValidator : AbstractValidator<UpdateStatusTitleNameCommand>
    {
        private readonly IStatusTitleRepository _statusTitleRepository;

        public UpdateStatusTitleNameCommandValidator(IStatusTitleRepository statusTitleRepository)
        {
            _statusTitleRepository = statusTitleRepository;

            Include(new NameValidation<UpdateStatusTitleNameCommand>(50));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _statusTitleRepository.ExistName(name))
                .WithMessage("Данное имя уже занято!");
            });
        }
    }
}
