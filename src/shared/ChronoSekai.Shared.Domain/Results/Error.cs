namespace ChronoSekai.Shared.Domain.Results
{
    public sealed record Error(ErrorCode ErrorCode, string Message)
    {
        /// <summary>
        /// Вы не указали '{prop}'!
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        public static Error Empty(string prop) => new(ErrorCode.Empty, $"Вы не указали '{prop}'!");
    }
}
