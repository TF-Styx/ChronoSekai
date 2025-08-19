using ChronoSekai.Shared.API.Application.Validations.Abstraction;
using FluentValidation;

namespace ChronoSekai.Shared.API.Application.Validations.Realization
{
    public sealed class NameValidation<TCommand> : AbstractValidator<TCommand> where TCommand : IHasName
    {
        public NameValidation(int MaxLength) 
            => RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Вы не указали наименование!")
            .MaximumLength(MaxLength).WithMessage($"Максимально допустимое значение для данного поля: '{MaxLength}'");
    }
}
