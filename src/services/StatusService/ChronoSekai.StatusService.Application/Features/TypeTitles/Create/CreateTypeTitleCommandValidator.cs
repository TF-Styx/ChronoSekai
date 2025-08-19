using FluentValidation;
using ChronoSekai.Shared.API.Application.Validations.Realization;
using ChronoSekai.StatusService.Application.InterfaceRepositories;

namespace ChronoSekai.StatusService.Application.Features.TypeTitles.Create
{
    public sealed class CreateTypeTitleCommandValidator : AbstractValidator<CreateTypeTitleCommand>
    {
        private readonly ITypeTitleRepository _typeTitleRepository;

        public CreateTypeTitleCommandValidator(ITypeTitleRepository typeTitleRepository)
        {
            _typeTitleRepository = typeTitleRepository;

            Include(new NameValidation<CreateTypeTitleCommand>(50));

            When(x => !string.IsNullOrWhiteSpace(x.Name), () =>
            {
                RuleFor(x => x.Name).MustAsync(async (name, clt) => !await _typeTitleRepository.ExistName(name))
                .WithMessage("Данное имя уже существует!");
            });
        }

        //private async Task<bool> Exist(string arg1, CancellationToken token)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
