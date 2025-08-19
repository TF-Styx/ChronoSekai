namespace ChronoSekai.Shared.Domain.Primitives
{
    public interface IDomainEvent
    {
        public Guid IdEvent { get; }
        public DateTime OccurredOnUts { get; }
    }
}
