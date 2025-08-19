using ChronoSekai.Shared.Domain.Exceptions.Guard;
using ChronoSekai.Shared.Domain.Primitives;

namespace ChronoSekai.StatusService.Domain.Models
{
    public sealed class StatusTranslate : AggregateRoot<int>
    {
        public string Name { get; private set; } = null!;

        private StatusTranslate() { }
        private StatusTranslate(string name) { Name = name; }

        public static StatusTranslate Create(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнил поле!");

            return new StatusTranslate(name);
        }

        public void UpdateName(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнил поле!");

            if (Name != name)
            {
                Name = name;

                //AddEvent(new UpdateStatusTranslateEvent(Guid.NewGuid(), DateTime.UtcNow, Id, Name));
            }
        }
    }
}
