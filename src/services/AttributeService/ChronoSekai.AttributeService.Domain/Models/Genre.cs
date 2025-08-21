using ChronoSekai.Shared.Domain.Exceptions.Guard;
using ChronoSekai.Shared.Domain.Primitives;

namespace ChronoSekai.AttributeService.Domain.Models
{
    public sealed class Genre : AggregateRoot<int>
    {
        public string Name { get; private set; } = null!;

        private Genre() { }
        private Genre(string name) { Name = name; }

        public static Genre Create(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнили поле!");

            return new (name);
        }

        public void UpdateName(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнил поле!");

            if (Name != name)
            {
                Name = name;

                //AddEvent(new UpdateGenreEvent(Guid.NewGuid(), DateTime.UtcNow, Id, Name));
            }
        }
    }
}
