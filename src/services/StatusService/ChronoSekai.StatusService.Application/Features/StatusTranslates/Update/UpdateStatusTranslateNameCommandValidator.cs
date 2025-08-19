using ChronoSekai.Shared.API.Application.Validations.Realization;
using ChronoSekai.StatusService.Application.InterfaceRepositories;
using FluentValidation;

namespace ChronoSekai.StatusService.Application.Features.StatusTranslates.Update
{
    internal class UpdateStatusTranslateNameCommandValidator : AbstractValidator<UpdateStatusTranslateNameCommand>
    {
        private readonly IStatusTranslateRepository _statusTranslateRepository;

        public UpdateStatusTranslateNameCommandValidator(IStatusTranslateRepository statusTranslateRepository)
        {
            _statusTranslateRepository = statusTranslateRepository;

            Include(new NameValidation<UpdateStatusTranslateNameCommand>(50));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _statusTranslateRepository.ExistName(name))
                .WithMessage("Данное имя уже занято!");
            });
        }
    }
}
