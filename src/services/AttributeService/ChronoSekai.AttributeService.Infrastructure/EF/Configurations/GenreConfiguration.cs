using ChronoSekai.AttributeService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChronoSekai.AttributeService.Infrastructure.EF.Configurations
{
    internal sealed class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .HasColumnName("Id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(g => g.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
