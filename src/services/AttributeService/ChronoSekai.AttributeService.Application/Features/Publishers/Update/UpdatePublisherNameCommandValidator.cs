using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using FluentValidation;

namespace ChronoSekai.AttributeService.Application.Features.Publishers.Update
{
    public sealed class UpdatePublisherNameCommandValidator : AbstractValidator<UpdatePublisherNameCommand>
    {
        private readonly IPublisherRepository _publisherRepository;

        public UpdatePublisherNameCommandValidator(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;

            Include(new NameValidation<UpdatePublisherNameCommand>(100));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _publisherRepository.ExistName(name))
                .WithMessage("Данное имя уже занято!");
            });
        }
    }
}
