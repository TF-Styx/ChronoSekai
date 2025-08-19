using ChronoSekai.Shared.Domain.Primitives;

namespace ChronoSekai.Shared.Contracts.Events.Statuses
{
    public sealed record UpdateStatusTranslateEvent
        (
            Guid IdEvent, DateTime OccurredOnUts,
            int IdStatusTitle, string StatusName
        ) : IDomainEvent;
}
