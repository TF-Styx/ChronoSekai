using System.Diagnostics.CodeAnalysis;

namespace ChronoSekai.Shared.Domain.Exceptions.Guard
{
    public static class NullGuardExtensions
    {
        public static T Null<T>(this IGuardClause _, [NotNull] T? input, string parameterName, string? message = null)
        {
            if (input is null)
                throw new ArgumentNullException(parameterName, message ?? $"Параметре {parameterName} не может быть null.");

            return input;
        }
    }
}
