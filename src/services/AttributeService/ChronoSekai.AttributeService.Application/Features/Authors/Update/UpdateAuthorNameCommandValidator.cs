using ChronoSekai.AttributeService.Application.InterfaceRepositories;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using FluentValidation;

namespace ChronoSekai.AttributeService.Application.Features.Authors.Update
{
    public class UpdateAuthorNameCommandValidator : AbstractValidator<UpdateAuthorNameCommand>
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorNameCommandValidator(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;

            Include(new NameValidation<UpdateAuthorNameCommand>(100));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _authorRepository.ExistName(name))
                .WithMessage("Данное имя уже занято!");
            });
        }
    }
}
