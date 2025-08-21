using ChronoSekai.Shared.Domain.Exceptions.Guard;
using ChronoSekai.Shared.Domain.Primitives;

namespace ChronoSekai.AttributeService.Domain.Models
{
    public class Author : AggregateRoot<int>
    {
        public string Name { get; private set; } = null!;

        private Author() { }
        private Author(string name) { Name = name; }

        public static Author Create(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнили поле!");

            return new Author(name);
        }

        public void UpdateName(string name)
        {
            GuardException.Against.Null(name, nameof(name), "Вы не заполнил поле!");

            if (Name != name)
            {
                Name = name;

                //AddEvent(new UpdateAuthorEvent(Guid.NewGuid(), DateTime.UtcNow, Id, Name));
            }
        }
    }
}
