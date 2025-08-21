using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using FluentValidation;

namespace ChronoSekai.AttributeService.Application.Features.Authors.Create
{
    public sealed class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;

        public CreateAuthorCommandValidator(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;

            Include(new NameValidation<CreateAuthorCommand>(100));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _authorRepository.ExistName(name))
                .WithMessage("Данное имя уже существует!");
            });
        }
    }
}
