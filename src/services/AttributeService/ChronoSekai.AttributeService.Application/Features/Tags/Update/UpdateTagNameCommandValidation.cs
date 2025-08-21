using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using FluentValidation;

namespace ChronoSekai.AttributeService.Application.Features.Tags.Update
{
    public sealed class UpdateTagNameCommandValidation : AbstractValidator<UpdateTagNameCommand>
    {
        private readonly ITagRepository _tagRepository;

        public UpdateTagNameCommandValidation(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;

            Include(new NameValidation<UpdateTagNameCommand>(100));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _tagRepository.ExistName(name))
                .WithMessage("Данное имя уже занято!");
            });
        }
    }
}
