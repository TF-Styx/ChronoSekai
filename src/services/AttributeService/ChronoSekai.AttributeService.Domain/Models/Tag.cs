using ChronoSekai.Shared.Domain.Exceptions.Guard;
using ChronoSekai.Shared.Domain.Primitives;

namespace ChronoSekai.AttributeService.Domain.Models
{
    public class Tag : AggregateRoot<int>
    {
        public string Name { get; private set; } = null!;

        private Tag() { }
        private Tag(string name) { Name = name; }

        public static Tag Create(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнили поле!");

            return new Tag(name);
        }

        public void UpdateName(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнил поле!");

            if (Name != name)
            {
                Name = name;

                //AddEvent(new UpdateTagEvent(Guid.NewGuid(), DateTime.UtcNow, Id, Name));
            }
        }
    }
}
