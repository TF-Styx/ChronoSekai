namespace ChronoSekai.Shared.Domain.Primitives
{
    public abstract class AggregateRoot<TId> : Entity<TId> where TId : notnull
    {
        private readonly List<IDomainEvent> _events = [];
        protected AggregateRoot() { }
        protected AggregateRoot(TId id) : base(id) { }

        public IReadOnlyCollection<IDomainEvent> GetEvents() => [.. _events];
        public void ClearEvents() => _events.Clear();
        protected void AddEvent(IDomainEvent domainEvent) => _events.Add(domainEvent);
    }
}
