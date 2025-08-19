using ChronoSekai.Shared.Domain.Exceptions.Guard;
using System.Text;

namespace ChronoSekai.Shared.Domain.Results
{
    public sealed class Result<TValue> : Result
    {
        private readonly TValue? _value;
        public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("Нельзя получить значение из не успешного результата!");

        private Result(TValue? value, bool isSuccess, List<Error> errors) : base(isSuccess, errors)
        {
            GuardException.Against.That(isSuccess && value is null && default(TValue) is not null, () => new InvalidOperationException("Нельзя создать успешный результат для типа значения Value Type не допускающего null, со значением null!"));

            _value = value;
        }

        public static Result<TValue> Success(TValue value) => new(value, true, []);
        public new static Result<TValue> Failure(Error error) => new(default, false, [error]);
        public new static Result<TValue> Failure(IEnumerable<Error> errors) => new(default, false, [..errors]);

        public TResult Match<TResult>(Func<TValue, TResult> onSuccess, Func<IReadOnlyList<Error>, TResult> onFailure) => IsSuccess ? onSuccess(Value) : onFailure(Errors);
        public void Switch(Action<TValue> onSuccess, Action<IReadOnlyList<Error>> onFailure)
        {
            if (IsSuccess)
                onSuccess(Value);
            else
                onFailure(Errors);
        }
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<Error> Errors { get; }

        protected Result(bool isSuccess, List<Error> errors)
        {
            GuardException.Against.That(isSuccess && errors.Any(), () => new InvalidOperationException("Нельзя создать успешный результат с ошибками!"));
            GuardException.Against.That(!isSuccess && !errors.Any(), () => new InvalidOperationException("Нельзя создать провальный результат без ошибок!"));

            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Success() => new(true, []);
        public static Result Failure(Error error) => new(false, [error]);
        public static Result Failure(IEnumerable<Error> errors) => new(false, [..errors]);

        public TResult Match<TResult>(Func<TResult> onSuccess, Func<IReadOnlyList<Error>, TResult> onFailure) => IsSuccess ? onSuccess() : onFailure(Errors);
        public void Switch(Action onSuccess, Action<IReadOnlyList<Error>> onFailure)
        {
            if (IsSuccess)
                onSuccess();
            else
                onFailure(Errors);
        }

        public string ErrorMessage => BuildMessage(error => $"Код: {(int)error.ErrorCode} - {error.ErrorCode}. Причина: {error.Message}");
        private string BuildMessage(Func<Error, string> messageSelector)
        {
            if (Errors.Count is 0)
                return "Ошибок нет!";

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Ошибки: ");

            for (int i = 0; i < Errors.Count; i++)
            {
                var message = messageSelector(Errors[i]);
                sb.Append($"{i++}) ");
                sb.AppendLine(message);
            }

            return sb.ToString();
        }

        public override string ToString() => ErrorMessage;
    }
}
