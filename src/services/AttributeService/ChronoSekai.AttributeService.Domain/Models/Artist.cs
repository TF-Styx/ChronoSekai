using ChronoSekai.Shared.Domain.Exceptions.Guard;
using ChronoSekai.Shared.Domain.Primitives;

namespace ChronoSekai.AttributeService.Domain.Models
{
    public class Artist : AggregateRoot<int>
    {
        public string Name { get; private set; } = null!;

        private Artist() { }
        private Artist(string name) { Name = name; }

        public static Artist Create(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнили поле!");

            return new Artist(name);
        }

        public void UpdateName(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнил поле!");

            if (Name != name)
            {
                Name = name;

                //AddEvent(new UpdateArtistEvent(Guid.NewGuid(), DateTime.UtcNow, Id, Name));
            }
        }
    }
}
