using ChronoSekai.Shared.Domain.Exceptions.Guard;
using ChronoSekai.Shared.Domain.Primitives;

namespace ChronoSekai.AttributeService.Domain.Models
{
    public class Publisher : AggregateRoot<int>
    {
        public string Name { get; private set; } = null!;

        private Publisher() { }
        private Publisher(string name) { Name = name; }

        public static Publisher Create(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнили поле!");

            return new Publisher(name);
        }

        public void UpdateName(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнил поле!");

            if (Name != name)
            {
                Name = name;

                //AddEvent(new UpdatePublisherEvent(Guid.NewGuid(), DateTime.UtcNow, Id, Name));
            }
        }
    }
}
