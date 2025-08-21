using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using FluentValidation;

namespace ChronoSekai.AttributeService.Application.Features.Tags.Create
{
    public sealed class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
    {
        private readonly ITagRepository _tagRepository;

        public CreateTagCommandValidator(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

            Include(new NameValidation<CreateTagCommand>(100));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _tagRepository.ExistName(name))
                .WithMessage("Данное имя уже занято!");
            });
        }
    }
}
