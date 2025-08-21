using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using FluentValidation;

namespace ChronoSekai.AttributeService.Application.Features.Publishers.Create
{
    public sealed class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
    {
        private readonly IPublisherRepository _publisherRepository;

        public CreatePublisherCommandValidator(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;

            Include(new NameValidation<CreatePublisherCommand>(100));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _publisherRepository.ExistName(name))
                .WithMessage("Данное имя уже занято!");
            });
        }
    }
}
