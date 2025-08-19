namespace ChronoSekai.Shared.Domain.Exceptions.Guard
{
    public static class GuardException
    {
        public static IGuardClause Against { get; } = new GuardClause();
    }
}
