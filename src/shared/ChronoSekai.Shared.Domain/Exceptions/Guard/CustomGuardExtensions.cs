namespace ChronoSekai.Shared.Domain.Exceptions.Guard
{
    public static class CustomGuardExtensions
    {
        /// <summary>
        /// Универсальный Guard для пользовательских условий. Срабатывает, если условие истинно.
        /// Использовать только для редких, специфичных проверок, для которых нет стандартного Guard-метода.
        /// </summary>
        /// <param name="_"></param>
        /// <param name="condition"></param>
        /// <param name="exceptionFactory"></param>
        public static void That(this IGuardClause _, bool condition, Func<Exception> exceptionFactory)
        {
            if (condition)
                throw exceptionFactory();
        }

        /// <summary>
        /// Универсальный Guard для пользовательских условий с простым сообщением.
        /// </summary>
        /// <param name="condition">Условие, которое, будучи истинным, вызовет исключение.</param>
        /// <param name="message">Сообщение для исключения.</param>
        /// <exception cref="ArgumentException">Исключение с указанным сообщением.</exception>
        public static void That(this IGuardClause _, bool condition, string message)
        {
            if (condition)
                throw new ArgumentException(message);
        }
    }
}
